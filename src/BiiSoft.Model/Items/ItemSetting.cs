using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiItemSettings")]
    public class ItemSetting : ActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool UseCodeFormula { get; private set; }
        public bool UseNetWeight { get; private set; }
        public bool UseGrossWeight { get; private set; }
        public bool UseWidth { get; private set; }
        public bool UseHeight { get; private set; }
        public bool UseLength { get; private set; }
        public bool UseDiameter { get; private set; }
        public bool UseArea { get; private set; }
        public bool UseVolume { get; private set; }
        public bool UseSerial { get; private set; }
        public bool UseExpired { get; private set; }
        public bool UseBatchNo { get; private set; }
        public bool UseInventoryStatus { get; private set; }

        public bool UseReorderStock { get; private set; }
        public bool UseMinStock { get; private set; }
        public bool UseMaxStock { get; private set; }

        public bool UseItemGroup { get; private set; }
        public bool UseBrand { get; private set; }
        public bool UseModel { get; private set; }
        public bool UseSeries { get; private set; }
        public bool UseSize { get; private set; }
        public bool UseGrade { get; private set; }
        public bool UseColorPattern { get; private set; }
        public bool UseCPU { get; private set; }
        public bool UseRAM { get; private set; }
        public bool UseVGA { get; private set; }
        public bool UseCamera { get; private set; }
        public bool UseScreen { get; private set; }
        public bool UseHDD { get; private set; }
        public bool UseBattery { get; private set; }
        public bool UseFieldA { get; private set; }
        public bool UseFieldB { get; private set; }
        public bool UseFieldC { get; private set; }
        public string FieldALabel { get; private set; }
        public string FieldBLabel { get; private set; }
        public string FieldCLabel { get; private set; }
        public bool NetWeightRequired { get; private set; }
        public bool GrossWeightRequired { get; private set; }
        public bool WidthRequired { get; private set; }
        public bool HeightRequired { get; private set; }
        public bool LengthRequired { get; private set; }
        public bool DiameterRequired { get; private set; }
        public bool AreaRequired { get; private set; }
        public bool VolumeRequired { get; private set; }
        public bool SerialRequired { get; private set; }
        public bool ExpiredRequired { get; private set; }
        public bool BatchNoRequired { get; private set; }
        public bool InventoryStatusRequired { get; private set; }

        public bool ReorderStockRequired { get; private set; }
        public bool MinStockRequired { get; private set; }
        public bool MaxStockRequired { get; private set; }

        public bool ItemGroupRequired { get; private set; }
        public bool BrandRequired { get; private set; }
        public bool ModelRequired { get; private set; }
        public bool SeriesRequired { get; private set; }
        public bool SizeRequired { get; private set; }
        public bool GradeRequired { get; private set; }
        public bool ColorPatternRequired { get; private set; }
        public bool CPURequired { get; private set; }
        public bool RAMRequired { get; private set; }
        public bool VGARequired { get; private set; }
        public bool CameraRequired { get; private set; }
        public bool ScreenRequired { get; private set; }
        public bool HDDRequired { get; private set; }
        public bool BatteryRequired { get; private set; }
        public bool FieldARequired { get; private set; }
        public bool FieldBRequired { get; private set; }
        public bool FieldCRequired { get; private set; }

        public static ItemSetting Create(
            int tenantId,
            long userId,
            bool useItemGroup,
            bool itemGroupRequired)
        {
            return new ItemSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                UseItemGroup = useItemGroup,
                ItemGroupRequired = itemGroupRequired,
                IsActive = true
            };
        }

        public static ItemSetting Create(
            int tenantId,
            long userId,
            bool useCodeFormula,
            bool useNetWeight,
            bool useGrossWeight,
            bool useWidth,
            bool useHeight,
            bool useLength,
            bool useDiameter,
            bool useArea,
            bool useVolume,
            bool useSerial,
            bool useExpired,
            bool useBatchNo,
            bool useInventoryStatus,
            bool useReorderStock,
            bool useMaxStock,
            bool useMinStock,
            bool useItemGroup,
            bool useBrand,
            bool useModel,
            bool useSeries,
            bool useSize,
            bool useGrade,
            bool useColorPattern,
            bool useCPU,
            bool useRAM,
            bool useVGA,
            bool useHDD,
            bool useScreen,
            bool useCamera,
            bool useBattery,
            bool useFieldA,
            bool useFieldB,
            bool useFieldC,
            string fieldALabel,
            string fieldBLabel,
            string fieldCLabel,
            bool netWeightRequired,
            bool grossWeightRequired,
            bool widthRequired,
            bool heightRequired,
            bool lengthRequired,
            bool diameterRequired,
            bool areaRequired,
            bool volumeRequired,
            bool serialRequired,
            bool expiredRequired,
            bool batchNoRequired,
            bool inventoryStatusRequired,
            bool reorderStockRequired,
            bool maxStockRequired,
            bool minStockRequired,
            bool itemGroupRequired,
            bool brandRequired,
            bool modelRequired,
            bool seriesRequired,
            bool sizeRequired,
            bool gradeRequired,
            bool colorPatternRequired,
            bool cpuRequired,
            bool ramRequired,
            bool vgaRequired,
            bool hddRequired,
            bool screenRequired,
            bool cameraRequired,
            bool batteryRequired,
            bool fieldARequired,
            bool fieldBRequired,
            bool fieldCRequired)
        {
            return new ItemSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                UseCodeFormula = useCodeFormula,
                UseNetWeight = useNetWeight,
                UseGrossWeight = useGrossWeight,
                UseWidth = useWidth,
                UseHeight = useHeight,
                UseLength = useLength,
                UseDiameter = useDiameter,
                UseArea = useArea,
                UseVolume = useVolume,
                UseSerial = useSerial,
                UseExpired = useExpired,
                UseBatchNo = useBatchNo,
                UseInventoryStatus = useInventoryStatus,
                UseReorderStock = useReorderStock,
                UseMaxStock = useMaxStock,
                UseMinStock = useMinStock,
                UseItemGroup = useItemGroup,
                UseBrand = useBrand,
                UseModel = useModel,
                UseSeries = useSeries,
                UseSize = useSize,
                UseGrade = useGrade,
                UseColorPattern = useColorPattern,
                UseCPU = useCPU,
                UseRAM = useRAM,
                UseVGA = useVGA,
                UseHDD = useHDD,
                UseScreen = useScreen,
                UseCamera = useCamera,
                UseBattery = useBattery,
                UseFieldA = useFieldA,
                UseFieldB = useFieldB,
                UseFieldC = useFieldC,
                FieldALabel = fieldALabel,
                FieldBLabel = fieldBLabel,
                FieldCLabel = fieldCLabel,
                NetWeightRequired = netWeightRequired,
                GrossWeightRequired = grossWeightRequired,
                WidthRequired = widthRequired,
                HeightRequired = heightRequired,
                LengthRequired = lengthRequired,
                DiameterRequired = diameterRequired,
                AreaRequired = areaRequired,
                VolumeRequired = volumeRequired,
                SerialRequired = serialRequired,
                ExpiredRequired = expiredRequired,
                BatchNoRequired = batchNoRequired,
                InventoryStatusRequired = inventoryStatusRequired,
                ReorderStockRequired = reorderStockRequired,
                MaxStockRequired = maxStockRequired,
                MinStockRequired = minStockRequired,
                ItemGroupRequired = itemGroupRequired,
                BrandRequired = brandRequired,
                ModelRequired = modelRequired,
                SeriesRequired = seriesRequired,
                SizeRequired = sizeRequired,
                GradeRequired = gradeRequired,
                ColorPatternRequired = colorPatternRequired,
                CPURequired = cpuRequired,
                RAMRequired = ramRequired,
                VGARequired = vgaRequired,
                HDDRequired = hddRequired,
                ScreenRequired = screenRequired,
                CameraRequired = cameraRequired,
                BatteryRequired = batteryRequired,
                FieldARequired = fieldARequired,
                FieldBRequired = fieldBRequired,
                FieldCRequired = fieldCRequired,
                IsActive = true
            };
        }

        public void Update(
            long userId,
            bool useCodeFormula,
            bool useNetWeight,
            bool useGrossWeight,
            bool useWidth,
            bool useHeight,
            bool useLength,
            bool useDiameter,
            bool useArea,
            bool useVolume,
            bool useSerial,
            bool useExpired,
            bool useBatchNo,
            bool useInventoryStatus,
            bool useReorderStock,
            bool useMaxStock,
            bool useMinStock,
            bool useItemGroup,
            bool useBrand,
            bool useModel,
            bool useSeries,
            bool useSize,
            bool useGrade,
            bool useColorPattern,
            bool useCPU,
            bool useRAM,
            bool useVGA,
            bool useHDD,
            bool useScreen,
            bool useCamera,
            bool useBattery,
            bool useFieldA,
            bool useFieldB,
            bool useFieldC,
            string fieldALabel,
            string fieldBLabel,
            string fieldCLabel,
            bool netWeightRequired,
            bool grossWeightRequired,
            bool widthRequired,
            bool heightRequired,
            bool lengthRequired,
            bool diameterRequired,
            bool areaRequired,
            bool volumeRequired,
            bool serialRequired,
            bool expiredRequired,
            bool batchNoRequired,
            bool inventoryStatusRequired,
            bool reorderStockRequired,
            bool maxStockRequired,
            bool minStockRequired,
            bool itemGroupRequired,
            bool brandRequired,
            bool modelRequired,
            bool seriesRequired,
            bool sizeRequired,
            bool gradeRequired,
            bool colorPatternRequired,
            bool cpuRequired,
            bool ramRequired,
            bool vgaRequired,
            bool hddRequired,
            bool screenRequired,
            bool cameraRequired,
            bool batteryRequired,
            bool fieldARequired,
            bool fieldBRequired,
            bool fieldCRequired)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            UseCodeFormula = useCodeFormula;
            UseNetWeight = useNetWeight;
            UseGrossWeight = useGrossWeight;
            UseWidth = useWidth;
            UseHeight = useHeight;
            UseLength = useLength;
            UseDiameter = useDiameter;
            UseArea = useArea;
            UseVolume = useVolume;
            UseSerial = useSerial;
            UseExpired = useExpired;
            UseBatchNo = useBatchNo;
            UseInventoryStatus = useInventoryStatus;
            UseReorderStock = useReorderStock;
            UseMaxStock = useMaxStock;
            UseMinStock = useMinStock;
            UseItemGroup = useItemGroup;
            UseBrand = useBrand;
            UseModel = useModel;
            UseSeries = useSeries;
            UseSize = useSize;
            UseGrade = useGrade;
            UseColorPattern = useColorPattern;
            UseCPU = useCPU;
            UseRAM = useRAM;
            UseVGA = useVGA;
            UseHDD = useHDD;
            UseScreen = useScreen;
            UseCamera = useCamera;
            UseBattery = useBattery;
            UseFieldA = useFieldA;
            UseFieldB = useFieldB;
            UseFieldC = useFieldC;
            FieldALabel = fieldALabel;
            FieldBLabel = fieldBLabel;
            FieldCLabel = fieldCLabel;
            NetWeightRequired = netWeightRequired;
            GrossWeightRequired = grossWeightRequired;
            WidthRequired = widthRequired;
            HeightRequired = heightRequired;
            LengthRequired = lengthRequired;
            DiameterRequired = diameterRequired;
            AreaRequired = areaRequired;
            VolumeRequired = volumeRequired;
            SerialRequired = serialRequired;
            ExpiredRequired = expiredRequired;
            BatchNoRequired = batchNoRequired;
            InventoryStatusRequired = inventoryStatusRequired;
            ReorderStockRequired = reorderStockRequired;
            MaxStockRequired = maxStockRequired;
            MinStockRequired = minStockRequired;
            ItemGroupRequired = itemGroupRequired;
            BrandRequired = brandRequired;
            ModelRequired = modelRequired;
            SeriesRequired = seriesRequired;
            SizeRequired = sizeRequired;
            GradeRequired = gradeRequired;
            ColorPatternRequired = colorPatternRequired;
            CPURequired = cpuRequired;
            RAMRequired = ramRequired;
            VGARequired = vgaRequired;
            HDDRequired = hddRequired;
            ScreenRequired = screenRequired;
            CameraRequired = cameraRequired;
            BatteryRequired = batteryRequired;
            FieldARequired = fieldARequired;
            FieldBRequired = fieldBRequired;
            FieldCRequired = fieldCRequired;
        }
    }
}
