using Abp.Collections.Extensions;
using BiiSoft.Items;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class ItemCodeFormulaManager : BiiSoftActiveValidateServiceBase<ItemCodeFormula, Guid>, IItemCodeFormulaManager
    {

        IBiiSoftRepository<ItemCodeFormulaItemType, Guid> _itemCodeFormulaItemTypeRepository;

        public ItemCodeFormulaManager(
            IBiiSoftRepository<ItemCodeFormulaItemType, Guid> itemCodeFormulaItemTypeRepository,
            IBiiSoftRepository<ItemCodeFormula, Guid> repository) 
        : base(repository)
        {
            _itemCodeFormulaItemTypeRepository = itemCodeFormulaItemTypeRepository;
        }

        protected override string InstanceName => L("ItemCodeFormula");

        protected override async Task ValidateInputAsync(ItemCodeFormula input)
        {
            if (input.IsAllItemType)
            {
                var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id);
                if (find) DuplicateException(InstanceName);
            }
            else
            {
                if (input.ItemTypes.IsNullOrEmpty()) SelectException(L("ItemType"));

                var duplicateType = input.ItemTypes.GroupBy(s => s.ItemType).Any(g => g.Count() > 1);
                if (duplicateType) DuplicateException(L("ItemType"));

                var types = input.ItemTypes.Select(s => s.ItemType).ToList();

                var find = await _repository.GetAll()
                                 .Include(s => s.ItemTypes)
                                 .AsNoTracking()
                                 .AnyAsync(s => s.Id != input.Id && (s.IsAllItemType || s.ItemTypes.Any(r => types.Contains(r.ItemType))));

                if (find) DuplicateException(InstanceName);
            }           
        }

        protected override ItemCodeFormula CreateInstance(ItemCodeFormula input)
        {
            return ItemCodeFormula.Create(input.TenantId, input.CreatorUserId.Value, input.IsAllItemType, input.Type, input.Prefix, input.Digits, input.Start);
        }

        protected override void UpdateInstance(ItemCodeFormula input, ItemCodeFormula entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.IsAllItemType, input.Type, input.Prefix, input.Digits, input.Start);
        }

        public override async Task<IdentityResult> InsertAsync(ItemCodeFormula input)
        {
            var result = await base.InsertAsync(input);

            if (!input.IsAllItemType)
            {
                await CurrentUnitOfWork.SaveChangesAsync();

                var itemTypes = input.ItemTypes.Select(s => ItemCodeFormulaItemType.Create(input.TenantId, input.CreatorUserId.Value, s.ItemType, input.Id)).ToList();
                await _itemCodeFormulaItemTypeRepository.BulkInsertAsync(itemTypes);
            }

            return result;
        }

        public override async Task<IdentityResult> UpdateAsync(ItemCodeFormula input)
        {
            var result = await base.UpdateAsync(input);

            var itemTypes = await _itemCodeFormulaItemTypeRepository.GetAll().AsNoTracking().Where(s => s.ItemCodeFormulaId == input.Id).ToListAsync();

            var addItemTypes = new List<ItemCodeFormulaItemType>();

            if (!input.IsAllItemType)
            {
                addItemTypes = input.ItemTypes.Where(s => !itemTypes.Any(r => r.ItemType == s.ItemType))
                               .Select(s => ItemCodeFormulaItemType.Create(input.TenantId, input.LastModifierUserId.Value, s.ItemType, input.Id))
                               .ToList();
            }

            var deleteItemTypes = input.IsAllItemType ? itemTypes : itemTypes.Where(s => !input.ItemTypes.Any(r => r.ItemType == s.ItemType)).ToList();

            if (addItemTypes.Any()) await _itemCodeFormulaItemTypeRepository.BulkInsertAsync(addItemTypes);
            if (deleteItemTypes.Any()) await _itemCodeFormulaItemTypeRepository.BulkDeleteAsync(deleteItemTypes);

            return result;
        }

        protected override async Task BeforeInstanceDeleteAsync(ItemCodeFormula entity)
        {
            var formulaItemTypes = await _itemCodeFormulaItemTypeRepository.GetAll().AsNoTracking().Where(s => s.ItemCodeFormulaId == entity.Id).ToListAsync();
            await _itemCodeFormulaItemTypeRepository.BulkDeleteAsync(formulaItemTypes);
        }

    }
}
