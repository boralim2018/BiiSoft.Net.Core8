namespace BiiSoft.Sessions.Dto
{
    public class ItemFieldSettingDto
    {
        public int TenantId { get; set; }
        public bool UseCode { get; set; }
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

    }
}
