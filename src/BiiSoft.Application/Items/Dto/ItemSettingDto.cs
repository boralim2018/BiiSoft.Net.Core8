using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Items.Dto
{
    public class ItemSettingDto
    {
        public Guid? Id { get; set; }
        public bool UseCodeFormula { get; set; }
        public bool UseNetWeight { get; set; }
        public bool UseGrossWeight { get; set; }
        public bool UseWidth { get; set; }
        public bool UseHeight { get; set; }
        public bool UseLength { get; set; }
        public bool UseDiameter { get; set; }
        public bool UseArea { get; set; }
        public bool UseVolume { get; set; }
        public bool UseSerial { get; set; }
        public bool UseExpired { get; set; }
        public bool UseBatchNo { get; set; }
        public bool UseInventoryStatus { get; set; }

        public bool UseReorderStock { get; set; }
        public bool UseMinStock { get; set; }
        public bool UseMaxStock { get; set; }

        public bool UseItemGroup { get; set; }
        public bool UseBrand { get; set; }
        public bool UseModel { get; set; }
        public bool UseSeries { get; set; }
        public bool UseSize { get; set; }
        public bool UseGrade { get; set; }
        public bool UseColorPattern { get; set; }
        public bool UseCPU { get; set; }
        public bool UseRAM { get; set; }
        public bool UseVGA { get; set; }
        public bool UseCamera { get; set; }
        public bool UseScreen { get; set; }
        public bool UseHDD { get; set; }
        public bool UseBattery { get; set; }
        public bool UseFieldA { get; set; }
        public bool UseFieldB { get; set; }
        public bool UseFieldC { get; set; }
        public string FieldALabel { get; set; }
        public string FieldBLabel { get; set; }
        public string FieldCLabel { get; set; }
        public bool NetWeightRequired { get; set; }
        public bool GrossWeightRequired { get; set; }
        public bool WidthRequired { get; set; }
        public bool HeightRequired { get; set; }
        public bool LengthRequired { get; set; }
        public bool DiameterRequired { get; set; }
        public bool AreaRequired { get; set; }
        public bool VolumeRequired { get; set; }
        public bool SerialRequired { get; set; }
        public bool ExpiredRequired { get; set; }
        public bool BatchNoRequired { get; set; }
        public bool InventoryStatusRequired { get; set; }

        public bool ReorderStockRequired { get; set; }
        public bool MinStockRequired { get; set; }
        public bool MaxStockRequired { get; set; }

        public bool ItemGroupRequired { get; set; }
        public bool BrandRequired { get; set; }
        public bool ModelRequired { get; set; }
        public bool SeriesRequired { get; set; }
        public bool SizeRequired { get; set; }
        public bool GradeRequired { get; set; }
        public bool ColorPatternRequired { get; set; }
        public bool CPURequired { get; set; }
        public bool RAMRequired { get; set; }
        public bool VGARequired { get; set; }
        public bool CameraRequired { get; set; }
        public bool ScreenRequired { get; set; }
        public bool HDDRequired { get; set; }
        public bool BatteryRequired { get; set; }
        public bool FieldARequired { get; set; }
        public bool FieldBRequired { get; set; }
        public bool FieldCRequired { get; set; }
    }
}
