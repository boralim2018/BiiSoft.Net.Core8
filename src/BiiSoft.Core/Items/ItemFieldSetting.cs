using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Extensions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Items
{
    [Table("BiiItemFieldSettings")]
    public class ItemFieldSetting : ActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool UseBrand { get; private set; }
        public bool UseModel { get; private set; }
        public bool UseSeries { get; private set; }
        public bool UseColorPattern { get; private set; }
        public bool UseSize { get; private set; }
        public bool UseGrade { get; private set; }
        public bool UseCPU { get; private set; }
        public bool UseRAM { get; private set; }
        public bool UseVGA { get; private set; }
        public bool UseCamera { get; private set; }
        public bool UseScreen { get; private set; }
        public bool UseStorage { get; private set; }
        public bool UseBattery { get; private set; }
        public bool UseOS { get; private set; }
        public bool UseFieldA { get; private set; }
        public bool UseFieldB { get; private set; }
        public bool UseFieldC { get; private set; }
        public string FieldALabel { get; private set; }
        public string FieldBLabel { get; private set; }
        public string FieldCLabel { get; private set; }

        public static ItemFieldSetting Create(
            int tenantId,
            long userId,
            bool useBrand,
            bool useModel,
            bool useSeries,
            bool useColorPattern,
            bool useSize,
            bool useGrade,
            bool useCPU,
            bool useRAM,
            bool useVGA,
            bool useStorage,
            bool useScreen,
            bool useCamera,
            bool useBattery,
            bool useOS,
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
                UseBrand = useBrand,
                UseModel = useModel,
                UseSeries = useSeries,
                UseColorPattern = useColorPattern,
                UseSize = useSize,
                UseGrade = useGrade,
                UseCPU = useCPU,
                UseRAM = useRAM,
                UseVGA = useVGA,
                UseStorage = useStorage,
                UseScreen = useScreen,
                UseCamera = useCamera,
                UseBattery = useBattery,
                UseOS = useOS,
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
            bool useBrand,
            bool useModel,
            bool useSeries,
            bool useColorPattern,
            bool useSize,
            bool useGrade,
            bool useCPU,
            bool useRAM,
            bool useVGA,
            bool useStorage,
            bool useScreen,
            bool useCamera,
            bool useBattery,
            bool useOS,
            bool useFieldA,
            bool useFieldB,
            bool useFieldC,
            string fieldALabel,
            string fieldBLabel,
            string fieldCLabel)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            UseBrand = useBrand;
            UseModel = useModel;
            UseSeries = useSeries;
            UseColorPattern = useColorPattern;
            UseSize = useSize;
            UseGrade = useGrade;
            UseCPU = useCPU;
            UseRAM = useRAM;
            UseVGA = useVGA;
            UseStorage = useStorage;
            UseScreen = useScreen;
            UseCamera = useCamera;
            UseBattery = useBattery;
            UseOS = useOS;
            UseFieldA = useFieldA;
            UseFieldB = useFieldB;
            UseFieldC = useFieldC;
            FieldALabel = fieldALabel;
            FieldBLabel = fieldBLabel;
            FieldCLabel = fieldCLabel;
        }
    }
}
