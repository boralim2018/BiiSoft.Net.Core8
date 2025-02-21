using BiiSoft.Items;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class ItemFieldSettingManager : BiiSoftValidateServiceBase<ItemFieldSetting, Guid>, IItemFieldSettingManager
    {
      
        public ItemFieldSettingManager(
            IBiiSoftRepository<ItemFieldSetting, Guid> repository) 
        : base(repository)
        {
        }

        protected override string InstanceName => L("ItemFieldSetting");

        protected override ItemFieldSetting CreateInstance(ItemFieldSetting input)
        {
            return ItemFieldSetting.Create(input.TenantId, input.CreatorUserId.Value, input.UseCode);
        }

        protected override void UpdateInstance(ItemFieldSetting input, ItemFieldSetting entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.UseCode);
        }

        protected override async Task ValidateInputAsync(ItemFieldSetting input)
        {
            await Task.Run(() => { });
        }

    }
}
