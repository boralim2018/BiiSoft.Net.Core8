using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Users;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.ItemCodeFormulas.Dto;
using BiiSoft.Items;
using Microsoft.EntityFrameworkCore;

namespace BiiSoft.ItemCodeFormulas
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class ItemCodeFormulaAppService : BiiSoftAppServiceBase, IItemCodeFormulaAppService
    {
        private readonly IItemCodeFormulaManager _itemCodeFormulaManager;
        private readonly IBiiSoftRepository<ItemCodeFormula, Guid> _itemCodeFormulaRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
       
        public ItemCodeFormulaAppService(
            IItemCodeFormulaManager itemCodeFormulaManager,
            IBiiSoftRepository<ItemCodeFormula, Guid> itemCodeFormulaRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _itemCodeFormulaManager=itemCodeFormulaManager;
            _itemCodeFormulaRepository=itemCodeFormulaRepository;
            _userRepository=userRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_ItemCodeFormulas_Create)]
        public async Task<Guid> Create(CreateUpdateItemCodeFormulaInputDto input)
        {
            var entity = MapEntity<ItemCodeFormula, Guid>(input);
            
            CheckErrors(await _itemCodeFormulaManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_ItemCodeFormulas_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _itemCodeFormulaManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_ItemCodeFormulas_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _itemCodeFormulaManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_ItemCodeFormulas_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _itemCodeFormulaManager.EnableAsync(entity));
        }

        
        [AbpAuthorize(PermissionNames.Pages_Setup_Items_ItemCodeFormulas_View, PermissionNames.Pages_Setup_Items_ItemCodeFormulas_Edit)]
        public async Task<ItemCodeFormulaDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = _itemCodeFormulaRepository.GetAll()
                        .Include(s => s.ItemTypes)
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new ItemCodeFormulaDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            ItemTypes = l.ItemTypes.Select(s => new ItemCodeFormulaItemTypeDto { ItemType = s.ItemType }).ToList(),
                            Type = l.Type,
                            TypeName = l.Type.ToString(),
                            Prefix = l.Prefix,
                            Digits = l.Digits,
                            Start = l.Start,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _itemCodeFormulaManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_Models)]
        public async Task<PagedResultDto<ItemCodeFormulaListDto>> GetList(PageItemCodeFormulaInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<ItemCodeFormulaListDto>> GetListHelper(PageItemCodeFormulaInputDto input)
        {   
            var query = _itemCodeFormulaRepository.GetAll()
                        .Include(s => s.ItemTypes)
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)));
                        

            var totalCount = await query.CountAsync();
            var items = new List<ItemCodeFormulaListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new ItemCodeFormulaListDto
                {
                    Id = l.Id,
                    No = l.No,
                    ItemTypes = l.ItemTypes.Select(s => s.ItemType.GetName()).ToList(),
                    Type = l.Type.ToString(),
                    Prefix = l.Prefix,
                    Digits = l.Digits,
                    Start = l.Start,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserId = l.LastModifierUserId,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<ItemCodeFormulaListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_ItemCodeFormulas_Edit)]
        public async Task Update(CreateUpdateItemCodeFormulaInputDto input)
        {
            var entity = MapEntity<ItemCodeFormula, Guid>(input);

            CheckErrors(await _itemCodeFormulaManager.UpdateAsync(entity));
        }
    }
}
