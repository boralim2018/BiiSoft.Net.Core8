﻿using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Roles;
using BiiSoft.Authorization.Users;
using BiiSoft.Editions;
using BiiSoft.MultiTenancy.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using BiiSoft.Dtos;
using BiiSoft.Roles.Dto;
using Abp.UI;
using Abp;
using Abp.Application.Features;
using BiiSoft.Editions.Dto;
using Abp.Timing;

namespace BiiSoft.MultiTenancy
{
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedTenantInputDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        public TenantAppService(
            IRepository<Tenant, int> repository,
            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,
            RoleManager roleManager,
            IAbpZeroDbMigrator abpZeroDbMigrator)
            : base(repository)
        {
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
        }

        public override async Task<TenantDto> GetAsync(EntityDto<int> input)
        {
            var tenant = await Repository.GetAll().Include(s => s.Edition).FirstOrDefaultAsync(s => s.Id == input.Id);

            var result = ObjectMapper.Map<TenantDto>(tenant);

            result.EditionName = tenant.Edition.Name;

            return result;
        }

        public override async Task<PagedResultDto<TenantDto>> GetAllAsync(PagedTenantInputDto input)
        {
            var query = this.Repository.GetAll()
                            .WhereIf(input.IsActive != null, s => s.IsActive == input.IsActive)
                            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), s =>
                                s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                s.TenancyName.ToLower().Contains(input.Keyword.ToLower()))
                            .AsNoTracking()
                            .Select(s => new TenantDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                TenancyName = s.TenancyName,
                                IsActive = s.IsActive,
                                EditionId = s.EditionId,
                                EditionName = s.EditionId.HasValue ? s.Edition.Name : "",
                            });

            var totalCount = await query.CountAsync();
            var items = new List<TenantDto>();

            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<TenantDto>(totalCount, items);
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_Create)]
        public override async Task<TenantDto> CreateAsync(CreateTenantDto input)
        {
            CheckCreatePermission();

            // Create tenant
            var tenant = ObjectMapper.Map<Tenant>(input);
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.

            // Create tenant database
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            // We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                // Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                // Grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                // Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress);
                await _userManager.InitializeOptionsAsync(tenant.Id);
                CheckErrors(await _userManager.CreateAsync(adminUser, BiiSoftConsts.DefaultAdminPassword));
                await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id

                // Assign admin user to role!
                CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return MapToEntityDto(tenant);
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_Edit)]
        public override async Task<TenantDto> UpdateAsync(TenantDto input)
        {
            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.Name = input.Name; 
            entity.TenancyName = input.TenancyName;
            entity.EditionId = input.EditionId;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _tenantManager.UpdateAsync(entity);

            return input;
        }


        protected override IQueryable<Tenant> CreateFilteredQuery(PagedTenantInputDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.TenancyName.Contains(input.Keyword) || x.Name.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            // Manually mapped since TenantDto contains non-editable properties too.
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_Delete)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_ChangeFeatures)]
        public async Task<GetTenantFeaturesEditOutput> GetTenantFeaturesForEdit(EntityDto input)
        {
            var features = FeatureManager.GetAll()
                           .Where(f => f.Scope.HasFlag(FeatureScopes.Tenant));
            var featureValues = await _tenantManager.GetFeatureValuesAsync(input.Id);

            return new GetTenantFeaturesEditOutput
            {
                Features = ObjectMapper.Map<List<FlatFeatureDto>>(features).OrderBy(f => f.DisplayName).ToList(),
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList()
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_ChangeFeatures)]
        public async Task UpdateTenantFeatures(UpdateTenantFeaturesInput input)
        {
            await _tenantManager.SetFeatureValuesAsync(input.Id, input.FeatureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_ChangeFeatures)]
        public async Task ResetTenantSpecificFeatures(EntityDto input)
        {
            await _tenantManager.ResetAllFeaturesAsync(input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_Enable)]
        public async Task Enable(EntityDto input)
        {
            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.IsActive = true;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _tenantManager.UpdateAsync(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_Tenants_Disable)]
        public async Task Disable(EntityDto input)
        {
            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.IsActive = false;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _tenantManager.UpdateAsync(entity);
        }
    }
}

