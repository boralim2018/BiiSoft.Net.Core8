using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiItemFieldSettings")]
    public class ItemFieldSetting : ActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool UseCode { get; private set; }
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

        public static ItemFieldSetting Create(
            int tenantId,
            long userId,
            bool useCode,
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
            string fieldCLabel)
        {
            return new ItemFieldSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                UseCode = useCode,
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
                IsActive = true
            };
        }

        public void Update(
            long userId,
            bool useCode,
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
            string fieldCLabel)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            UseCode = useCode;
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
        }
    }
}
