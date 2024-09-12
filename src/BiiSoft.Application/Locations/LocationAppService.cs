using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.Locations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Extensions;
using BiiSoft.Authorization.Users;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Locations
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class LocationAppService : BiiSoftExcelAppServiceBase, ILocationAppService
    {
        private readonly ILocationManager _locationManager;
        private readonly IBiiSoftRepository<Location, Guid> _locationRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LocationAppService(
            IUnitOfWorkManager unitOfWorkManager,
            ILocationManager locationManager,
            IBiiSoftRepository<Location, Guid> locationRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _locationManager=locationManager;
            _locationRepository=locationRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Create)]
        public async Task<Guid> Create(CreateUpdateLocationInputDto input)
        {
            var entity = MapEntity<Location, Guid>(input);

            CheckErrors(await _locationManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _locationManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _locationManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _locationManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Locations)]
        public async Task<PagedResultDto<FindLocationDto>> Find(PageLocationInputDto input)
        {
            var query = _locationRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<FindLocationDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindLocationDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindLocationDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_View, PermissionNames.Pages_Setup_Locations_Edit)]
        public async Task<LocationDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = _locationRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new LocationDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            IsActive = l.IsActive,
                            Latitude = l.Latitude,
                            Longitude = l.Longitude,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _locationManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_List)]
        public async Task<PagedResultDto<LocationListDto>> GetList(PageLocationInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<LocationListDto>> GetListHelper(PageLocationInputDto input)
        {
            var query = _locationRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<LocationListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new LocationListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    CannotDelete = l.CannotDelete,
                    CannotEdit = l.CannotEdit,
                    IsActive = l.IsActive,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserId = l.LastModifierUserId,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<LocationListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelLocationInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            
            PagedResultDto<LocationListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "Location.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _locationManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _locationManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Edit)]
        public async Task Update(CreateUpdateLocationInputDto input)
        {
            var entity = MapEntity<Location, Guid>(input);

            CheckErrors(await _locationManager.UpdateAsync(entity));
        }
    }
}
