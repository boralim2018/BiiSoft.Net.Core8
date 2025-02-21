using BiiSoft.Items;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class ItemSettingManager : BiiSoftValidateServiceBase<ItemSetting, Guid>, IItemSettingManager
    {
      
        public ItemSettingManager(
            IBiiSoftRepository<ItemSetting, Guid> repository) 
        : base(repository)
        {
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
                input.UseInventoryStatus,
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
                input.SerialRequired,
                input.ExpiredRequired,
                input.BatchNoRequired,
                input.InventoryStatusRequired,
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
                input.FieldCRequired);
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
                input.UseInventoryStatus,
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
                input.SerialRequired,
                input.ExpiredRequired,
                input.BatchNoRequired,
                input.InventoryStatusRequired,
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
                input.FieldCRequired);
        }

        protected override async Task ValidateInputAsync(ItemSetting input)
        {
            await Task.Run(() => { });
        }

    }
}
