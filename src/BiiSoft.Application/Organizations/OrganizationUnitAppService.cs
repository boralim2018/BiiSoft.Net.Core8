﻿using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using BiiSoft.Authorization;
using BiiSoft.Organizations.Dto;
using System.Linq.Dynamic.Core;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using BiiSoft;
using BiiSoft.Organizations;
using System.Collections.Generic;

namespace CorarlERP.Organizations
{
    [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits)]
    public class OrganizationUnitAppService : BiiSoftAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var query =
                from ou in _organizationUnitRepository.GetAll()
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var items = await query.ToListAsync();

            return new ListResultDto<OrganizationUnitDto>(
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitDto>(item.ou);
                    dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }

        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        join user in UserManager.Users on uou.UserId equals user.Id
                        where uou.OrganizationUnitId == input.Id
                        //select new { uou, user };
                        select new OrganizationUnitUserListDto
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Name = user.Name,
                            Surname = user.Surname,
                            ProfilePictureId = null,
                            EmailAddress = user.EmailAddress,
                            AddedTime = uou.CreationTime,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<OrganizationUnitUserListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            //return new PagedResultDto<OrganizationUnitUserListDto>(
            //    totalCount,
            //    items.Select(item =>
            //    {
            //        var dto = ObjectMapper.Map<OrganizationUnitUserListDto>(item.user);
            //        dto.AddedTime = item.uou.CreationTime;
            //        return dto;
            //    }).ToList());

            return new PagedResultDto<OrganizationUnitUserListDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnit(EntityDto<long> input)
        {
            await _organizationUnitManager.DeleteAsync(input.Id);
        }


        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageMembers)]
        public async Task AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input)
        {
            foreach (var userId in input.UserIds)
            {
                await UserManager.AddToOrganizationUnitAsync(userId, input.OrganizationUnitId);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_OrganizationUnits_ManageMembers)]
        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input)
        {
            var userIdsInOrganizationUnit = _userOrganizationUnitRepository.GetAll()
                .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(uou => uou.UserId);

            var query = UserManager.Users
                .Where(u => !userIdsInOrganizationUnit.Contains(u.Id))
                .WhereIf(
                    !input.Keyword.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Keyword) ||
                        u.Surname.Contains(input.Keyword) ||
                        u.UserName.Contains(input.Keyword) ||
                        u.EmailAddress.Contains(input.Keyword)
                );

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Name)
                .ThenBy(u => u.Surname)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<NameValueDto>(
                userCount,
                users.Select(u =>
                    new NameValueDto(
                        u.FullName + " (" + u.EmailAddress + ")",
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }

        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
            dto.MemberCount = await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);
            return dto;
        }
    }
}