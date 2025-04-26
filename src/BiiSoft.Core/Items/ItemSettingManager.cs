using BiiSoft.ChartOfAccounts;
using BiiSoft.Extensions;
using BiiSoft.Items;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class ItemSettingManager : BiiSoftValidateServiceBase<ItemSetting, Guid>, IItemSettingManager
    {
      
        private readonly IBiiSoftRepository<ChartOfAccount, Guid> _chartOfAccountsRepository;
        public ItemSettingManager(
            IBiiSoftRepository<ChartOfAccount, Guid> chartOfAccountsRepository,
            IBiiSoftRepository<ItemSetting, Guid> repository) 
        : base(repository)
        {
            _chartOfAccountsRepository = chartOfAccountsRepository;
        }

        protected override string InstanceName => L("ItemSetting");

        protected override ItemSetting CreateInstance(ItemSetting input)
        {
            return ItemSetting.Create(
                input.TenantId,
                input.CreatorUserId.Value,
                input.UseCodeFormula,
                input.UseNetWeight,
                input.UseGrossWeight,
                input.UseWidth,
                input.UseHeight,
                input.UseLength,
                input.UseDiameter,
                input.UseArea,
                input.UseVolume,
                input.UseSerial,
                input.UseExpired,
                input.UseBatchNo,
                input.UseAssetStatus,
                input.UseReorderStock,
                input.UseMaxStock,
                input.UseMinStock,
                input.UseItemGroup,
                input.UseBrand,
                input.UseModel,
                input.UseSeries,
                input.UseSize,
                input.UseGrade,
                input.UseColorPattern,
                input.UseCPU,
                input.UseRAM,
                input.UseVGA,
                input.UseHDD,
                input.UseScreen,
                input.UseCamera,
                input.UseBattery,
                input.UseFieldA,
                input.UseFieldB,
                input.UseFieldC,
                input.FieldALabel,
                input.FieldBLabel,
                input.FieldCLabel,
                input.NetWeightRequired,
                input.GrossWeightRequired,
                input.WidthRequired,
                input.HeightRequired,
                input.LengthRequired,
                input.DiameterRequired,
                input.AreaRequired,
                input.VolumeRequired,
                input.ReorderStockRequired,
                input.MaxStockRequired,
                input.MinStockRequired,
                input.ItemGroupRequired,
                input.BrandRequired,
                input.ModelRequired,
                input.SeriesRequired,
                input.SizeRequired,
                input.GradeRequired,
                input.ColorPatternRequired,
                input.CPURequired,
                input.RAMRequired,
                input.VGARequired,
                input.HDDRequired,
                input.ScreenRequired,
                input.CameraRequired,
                input.BatteryRequired,
                input.FieldARequired,
                input.FieldBRequired,
                input.FieldCRequired,
                input.WeightUnit,
                input.LengthUnit,
                input.AreaUnit,
                input.VolumeUnit,
                input.InventoryAccountId,
                input.AssetAccountId,
                input.ExpenseAccountId,
                input.COGSAccountId,
                input.RevenueAccountId);
        }

        protected override void UpdateInstance(ItemSetting input, ItemSetting entity)
        {
            entity.Update(
                input.LastModifierUserId.Value,
                input.UseCodeFormula,
                input.UseNetWeight,
                input.UseGrossWeight,
                input.UseWidth,
                input.UseHeight,
                input.UseLength,
                input.UseDiameter,
                input.UseArea,
                input.UseVolume,
                input.UseSerial,
                input.UseExpired,
                input.UseBatchNo,
                input.UseAssetStatus,
                input.UseReorderStock,
                input.UseMaxStock,
                input.UseMinStock,
                input.UseItemGroup,
                input.UseBrand,
                input.UseModel,
                input.UseSeries,
                input.UseSize,
                input.UseGrade,
                input.UseColorPattern,
                input.UseCPU,
                input.UseRAM,
                input.UseVGA,
                input.UseHDD,
                input.UseScreen,
                input.UseCamera,
                input.UseBattery,
                input.UseFieldA,
                input.UseFieldB,
                input.UseFieldC,
                input.FieldALabel,
                input.FieldBLabel,
                input.FieldCLabel,
                input.NetWeightRequired,
                input.GrossWeightRequired,
                input.WidthRequired,
                input.HeightRequired,
                input.LengthRequired,
                input.DiameterRequired,
                input.AreaRequired,
                input.VolumeRequired,
                input.ReorderStockRequired,
                input.MaxStockRequired,
                input.MinStockRequired,
                input.ItemGroupRequired,
                input.BrandRequired,
                input.ModelRequired,
                input.SeriesRequired,
                input.SizeRequired,
                input.GradeRequired,
                input.ColorPatternRequired,
                input.CPURequired,
                input.RAMRequired,
                input.VGARequired,
                input.HDDRequired,
                input.ScreenRequired,
                input.CameraRequired,
                input.BatteryRequired,
                input.FieldARequired,
                input.FieldBRequired,
                input.FieldCRequired,
                input.WeightUnit,
                input.LengthUnit,
                input.AreaUnit,
                input.VolumeUnit,
                input.InventoryAccountId,
                input.AssetAccountId,
                input.ExpenseAccountId,
                input.COGSAccountId,
                input.RevenueAccountId);
        }

        protected override async Task ValidateInputAsync(ItemSetting input)
        {
            if (!input.InventoryAccountId.IsNullOrEmpty())
            {
                var findInventoryAccount = await _chartOfAccountsRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.InventoryAccountId);
                if(!findInventoryAccount) InvalidException(L("InventoryAccount"));
            }

            if (!input.AssetAccountId.IsNullOrEmpty())
            {
                var findAssetAccount = await _chartOfAccountsRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.AssetAccountId);
                if (!findAssetAccount) InvalidException(L("AssetAccount"));
            }

            if (!input.ExpenseAccountId.IsNullOrEmpty())
            {
                var findExpenseAccount = await _chartOfAccountsRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ExpenseAccountId);
                if (!findExpenseAccount) InvalidException(L("ExpenseAccount"));
            }

            if (!input.COGSAccountId.IsNullOrEmpty())
            {
                var findCOGSAccount = await _chartOfAccountsRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.COGSAccountId);
                if (!findCOGSAccount) InvalidException(L("COGSAccount"));
            }

            if (!input.RevenueAccountId.IsNullOrEmpty())
            {
                var findRevenueAccount = await _chartOfAccountsRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.RevenueAccountId);
                if (!findRevenueAccount) InvalidException(L("RevenueAccount"));
            }
        }

    }
}
