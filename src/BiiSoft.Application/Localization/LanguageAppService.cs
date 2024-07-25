using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.Localization.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BiiSoft.Localization
{
    [AbpAuthorize(PermissionNames.Pages_Languages)]
    public class LanguageAppService : BiiSoftAppServiceBase, ILanguageAppService
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;
        private readonly IRepository<ApplicationLanguage> _languageRepository;

        public LanguageAppService(
            IApplicationLanguageManager applicationLanguageManager,
            IApplicationLanguageTextManager applicationLanguageTextManager,
            IRepository<ApplicationLanguage> languageRepository)
        {
            _applicationLanguageManager = applicationLanguageManager;
            _languageRepository = languageRepository;
            _applicationLanguageTextManager = applicationLanguageTextManager;
        }

        public async Task<PagedLanguagesResultDto> GetLanguages(PageLanguageInputDto input)
        {
            var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
       
            var languages = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), s =>
                                s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                            .Select(s => s);

            var items = new List<ApplicationLanguage>();

            if (input.UsePagination)
            {
              items = languages.AsQueryable().OrderBy(input.GetOrdering()).PageBy(input).ToList();
            }
            else
            {
                items = languages.AsQueryable().OrderBy(input.GetOrdering()).ToList();
            }

            return new PagedLanguagesResultDto(
                languages.Count(),
                ObjectMapper.Map<List<ApplicationLanguageListDto>>(items),
                defaultLanguage?.Name);
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Create, PermissionNames.Pages_Languages_Edit)]
        public async Task<GetLanguageForEditOutput> GetLanguageForEdit(NullableIdDto input)
        {
            ApplicationLanguage language = null;
            if (input.Id.HasValue)
            {
                language = await _languageRepository.GetAsync(input.Id.Value);
            }

            var output = new GetLanguageForEditOutput();

            //Language
            output.Language = language != null
                ? ObjectMapper.Map<ApplicationLanguageEditDto>(language)
                : new ApplicationLanguageEditDto();

            //Language names
            output.LanguageNames = CultureHelper
                .AllCultures
                .Select(c => new ComboboxItemDto(c.Name, c.EnglishName) { IsSelected = output.Language.Name == c.Name })
                .ToList();

            //Flags
            output.Flags = FlagsHelper
                .Flags
                .OrderBy(f => f)
                .Select(f => new ComboboxItemDto(f, f) { IsSelected = output.Language.Icon == f })
                .ToList();

            return output;
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Create, PermissionNames.Pages_Languages_Edit)]
        public async Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input)
        {
            if (input.Language.Id.HasValue)
            {
                await UpdateLanguageAsync(input);
            }
            else
            {
                await CreateLanguageAsync(input);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Delete)]
        public async Task DeleteLanguage(EntityDto input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);

            if (language != null && defaultLanguage != null && language.Id == defaultLanguage.Id) 
                throw new UserFriendlyException(L("CannotBeDeleted", L("DefaultLanguage")));

            await _applicationLanguageManager.RemoveAsync(AbpSession.TenantId, language.Name);
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Enable)]
        public async Task Enable(EntityDto input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            language.IsDisabled = false;
            language.LastModificationTime= Abp.Timing.Clock.Now;
            language.LastModifierUserId = AbpSession.UserId;
            await _languageRepository.UpdateAsync(language);
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Disable)]
        public async Task Disable(EntityDto input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            language.IsDisabled = true;
            language.LastModificationTime = Abp.Timing.Clock.Now;
            language.LastModifierUserId = AbpSession.UserId;
            await _languageRepository.UpdateAsync(language);
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Edit)]
        public async Task SetDefaultLanguage(SetDefaultLanguageInput input)
        {
            await _applicationLanguageManager.SetDefaultLanguageAsync(
                AbpSession.TenantId,
                CultureHelper.GetCultureInfoByChecking(input.Name).Name
                );
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_ChangeTexts)]
        public async Task<PagedResultDto<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input)
        {
            /* Note: This method is used by SPA without paging, MPA with paging.
             * So, it can both usable with paging or not */

            //Normalize base language name
            if (input.BaseLanguageName.IsNullOrEmpty())
            {
                var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
                if (defaultLanguage == null)
                {
                    defaultLanguage = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).FirstOrDefault();
                    if (defaultLanguage == null)
                    {
                        throw new Exception("No language found in the application!");
                    }
                }

                input.BaseLanguageName = defaultLanguage.Name;
            }

            var source = LocalizationManager.GetSource(input.SourceName);
            var baseCulture = CultureInfo.GetCultureInfo(input.BaseLanguageName);
            var targetCulture = CultureInfo.GetCultureInfo(input.TargetLanguageName);

            var languageTexts = source
                .GetAllStrings()
                .Select(localizedString => new LanguageTextListDto
                {
                    Key = localizedString.Name,
                    BaseValue = _applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, baseCulture, localizedString.Name),
                    TargetValue = _applicationLanguageTextManager.GetStringOrNull(AbpSession.TenantId, source.Name, targetCulture, localizedString.Name, false)
                })
                .AsQueryable();

            //Filters
            if (input.TargetValueFilter == "EMPTY")
            {
                languageTexts = languageTexts.Where(s => s.TargetValue.IsNullOrEmpty());
            }

            if (!input.Keyword.IsNullOrEmpty())
            {
                languageTexts = languageTexts.Where(
                    l => (l.Key != null && l.Key.IndexOf(input.Keyword, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                         (l.BaseValue != null && l.BaseValue.IndexOf(input.Keyword, StringComparison.CurrentCultureIgnoreCase) >= 0) ||
                         (l.TargetValue != null && l.TargetValue.IndexOf(input.Keyword, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    );
            }

            var totalCount = languageTexts.Count();
            var items = new List<LanguageTextListDto>();
            if (totalCount == 0) return new PagedResultDto<LanguageTextListDto>(totalCount, items);
            
            if (input.UsePagination)
            {
                items = languageTexts.OrderBy(input.GetOrdering()).PageBy(input).ToList();
            }
            else
            {
                items = languageTexts.OrderBy(input.GetOrdering()).ToList();
            }

            return new PagedResultDto<LanguageTextListDto>(totalCount, items);
        }


        [AbpAuthorize(PermissionNames.Pages_Languages_ChangeTexts)]
        public async Task UpdateLanguageText(UpdateLanguageTextInput input)
        {
            var culture = CultureHelper.GetCultureInfoByChecking(input.LanguageName);
            var source = LocalizationManager.GetSource(input.SourceName);
            await _applicationLanguageTextManager.UpdateStringAsync(AbpSession.TenantId, source.Name, culture, input.Key, input.Value);
        }


        [AbpAuthorize(PermissionNames.Pages_Languages_Create)]
        protected virtual async Task CreateLanguageAsync(CreateOrUpdateLanguageInput input)
        {
            if (AbpSession.MultiTenancySide != MultiTenancySides.Host)
            {
                throw new UserFriendlyException(L("TenantsCannotCreateLanguage"));
            }

            var culture = CultureHelper.GetCultureInfoByChecking(input.Language.Name);

            await CheckLanguageIfAlreadyExists(culture.Name);

            await _applicationLanguageManager.AddAsync(
                new ApplicationLanguage(
                    AbpSession.TenantId,
                    culture.Name,
                    culture.DisplayName,
                    input.Language.Icon,
                    true
                )
            );
        }

        [AbpAuthorize(PermissionNames.Pages_Languages_Edit)]
        protected virtual async Task UpdateLanguageAsync(CreateOrUpdateLanguageInput input)
        {
            Debug.Assert(input.Language.Id != null, "input.Language.Id != null");

            var culture = CultureHelper.GetCultureInfoByChecking(input.Language.Name);

            await CheckLanguageIfAlreadyExists(culture.Name, input.Language.Id.Value);

            var language = await _languageRepository.GetAsync(input.Language.Id.Value);

            language.Name = culture.Name;
            language.DisplayName = culture.DisplayName;
            language.Icon = input.Language.Icon;

            await _applicationLanguageManager.UpdateAsync(AbpSession.TenantId, language);
        }

        private async Task CheckLanguageIfAlreadyExists(string languageName, int? expectedId = null)
        {
            var existingLanguage = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                .FirstOrDefault(l => l.Name == languageName);

            if (existingLanguage == null)
            {
                return;
            }

            if (expectedId != null && existingLanguage.Id == expectedId.Value)
            {
                return;
            }

            throw new UserFriendlyException(L("ThisLanguageAlreadyExists"));
        }
    }
}