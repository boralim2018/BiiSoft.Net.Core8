using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;
using BiiSoft.Enums;
using Microsoft.EntityFrameworkCore;
using BiiSoft.ChartOfAccounts;
using BiiSoft.Taxes;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
   
    [Table("BiiItems")]
    public class Item : NameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        [Required]
        public ItemType ItemType { get; private set; }
        public ItemCategory? ItemCategory { get; private set; }
        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        [StringLength(BiiSoftConsts.MaxLengthLongCode, ErrorMessage = BiiSoftConsts.MaxLengthLongCodeErrorMessage)]
        public string Code { get; private set; }

        public decimal NetWeight { get; private set; }
        public decimal GrossWeight { get; private set; }
        public decimal Length { get; private set; }
        public decimal Width { get; private set; }
        public decimal Height { get; private set; }
        public decimal Diameter { get; private set; }
        public decimal Area { get; private set; }
        public decimal Volume { get; private set; }

        public WeightUnit WeightUnit { get; private set; }
        public LengthUnit LengthUnit { get; private set; }
        public AreaUnit AreaUnit { get; private set; }
        public VolumeUnit VolumeUnit { get; private set; }

        public bool TrackSerial { get; private set; }
        public bool TrackExpired { get; private set; }
        public bool TrackBatchNo { get; private set; }
        public bool TrackInventoryStatus { get; private set; }

        public decimal ReorderStock { get; private set; }
        public decimal MinStock { get; private set; }
        public decimal MaxStock { get; private set; }

        public Guid? ItemGroupId { get; private set; }
        public ItemGroup ItemGroup { get; private set; }
        public Guid? ItemBrandId { get; private set; }
        public ItemBrand ItemBrand { get; private set; }
        public Guid? ItemGradeId { get; private set; }
        public ItemGrade ItemGrade { get; private set; }
        public Guid? ItemSizeId { get; private set; }
        public ItemSize ItemSize { get; private set; }
        public Guid? UnitId { get; private set; }
        public Unit Unit { get; private set; }
        public Guid? ColorPatternId { get; private set; }
        public ColorPattern ColorPattern { get; private set; }
        public Guid? ItemSeriesId { get; private set; }
        public ItemSeries ItemSeries { get; private set; }
        public Guid? ItemModelId { get; private set; }
        public ItemModel ItemModel { get; private set; }
        public Guid? CPUId { get; private set; }
        public CPU CPU { get; private set; }
        public Guid? RAMId { get; private set; }
        public RAM RAM { get; private set; }
        public Guid? VGAId { get; private set; }
        public VGA VGA { get; private set; }
        public Guid? HDDId { get; private set; }
        public HDD HDD { get; private set; }
        public Guid? BatteryId { get; private set; }
        public Battery Battery { get; private set; }
        public Guid? CameraId { get; private set; }
        public Camera Camera { get; private set; }
        public Guid? ScreenId { get; private set; }
        public Screen Screen { get; private set; }
        public Guid? FieldAId { get; private set; }
        public FieldA FieldA { get; private set; }
        public Guid? FieldBId { get; private set; }
        public FieldB FieldB { get; private set; }
        public Guid? FieldCId { get; private set; }
        public FieldC FieldC { get; private set; }

        public Guid? ImageId { get; private set; }
        public void SetImage(Guid? imageId) => ImageId = imageId;

        public Guid? PurchaseTaxId { get; private set; }
        public Tax PurchaseTax { get; private set; }
        public void SetPurchaseTax(Guid? taxId) => PurchaseTaxId = taxId;
        public Guid? SaleTaxId { get; private set; }
        public Tax SaleTax { get; private set; }
        public void SetSaleTax(Guid? taxId) => SaleTaxId = taxId;

        public Guid? PurchaseAccountId { get; private set; }
        public ChartOfAccount PurchaseAccount { get; private set; }
        public Guid? SaleAccountId { get; private set; }
        public ChartOfAccount SaleAccount { get; private set; }

        public Guid? InventoryAccountId { get; private set; }
        public ChartOfAccount InventoryAccount { get; private set; }


        [MaxLength(BiiSoftConsts.MaxLengthLongDescription)]
        [StringLength(BiiSoftConsts.MaxLengthLongDescription, ErrorMessage = BiiSoftConsts.MaxLengthLongDescriptionErrorMessage)]
        public string Description { get; private set; }

        public static Item Create(
            int tenantId,
            long userId,
            ItemType itemType,
            ItemCategory? itemCategory,
            string code,
            string name,
            string displayName,
            string description,
            decimal reorderStock,
            decimal minStock,
            decimal maxStock,
            decimal netWeight,
            decimal grossWeight,
            decimal width,
            decimal height,
            decimal length,
            decimal diameter,
            decimal area,
            decimal volume,
            WeightUnit weightUnit,
            LengthUnit lengthUnit,
            AreaUnit areaUnit,
            VolumeUnit volumeUnit,
            bool trackSerial,
            bool trackExpired,
            bool trackBatchNo,
            bool trackInventoryStatus,
            Guid? itemGroupId,
            Guid? itemBrandId,
            Guid? itemGradeId,
            Guid? itemSizeId,
            Guid? colorPatternId,
            Guid? unitId,
            Guid? itemSeriesId,
            Guid? itemModelId,
            Guid? cpuId,
            Guid? ramId,
            Guid? vgaId,
            Guid? screenId,
            Guid? batteryId,
            Guid? cameraId,
            Guid? hddId,
            Guid? fieldAId,
            Guid? fieldBId,
            Guid? fieldCId,
            Guid? inventoryAccountId,
            Guid? purchaseAccountId,
            Guid? saleAccountId,
            Guid? purchaseTaxId,
            Guid saleTaxId)
        {
            return new Item
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                ItemType = itemType,
                ItemCategory = itemCategory,
                Code = code,
                Name = name,
                DisplayName = displayName,
                Description = description,
                ReorderStock = reorderStock,
                MinStock = minStock,
                MaxStock = maxStock,
                NetWeight = netWeight,
                GrossWeight = grossWeight,
                Width = width,
                Height = height,
                Length = length,
                Diameter = diameter,
                Area = area,
                Volume = volume,
                WeightUnit = weightUnit,
                LengthUnit = lengthUnit,
                AreaUnit = areaUnit,
                VolumeUnit = volumeUnit,
                TrackSerial = trackSerial,
                TrackExpired = trackExpired,
                TrackBatchNo = trackBatchNo,
                TrackInventoryStatus = trackInventoryStatus,
                ItemGroupId = itemGroupId,
                ItemBrandId = itemBrandId,
                ItemGradeId = itemGradeId,
                ItemSizeId = itemSizeId,
                ColorPatternId = colorPatternId,
                UnitId = unitId,
                ItemSeriesId = itemSeriesId,
                ItemModelId = itemModelId,
                CPUId = cpuId,
                RAMId = ramId,
                VGAId = vgaId,
                ScreenId = screenId,
                BatteryId = batteryId,
                CameraId = cameraId,
                HDDId = hddId,
                FieldAId = fieldAId,
                FieldBId = fieldBId,
                FieldCId = fieldCId,
                InventoryAccountId = inventoryAccountId,
                PurchaseAccountId = purchaseAccountId,
                SaleAccountId = saleAccountId,
                PurchaseTaxId = purchaseTaxId,
                SaleTaxId = saleTaxId,
                IsActive = true
            };
        }

        public void Update(
            long userId,
            ItemType itemType,
            string code,
            string name,
            string displayName,
            string description,
            decimal reorderStock,
            decimal minStock,
            decimal maxStock,
            decimal netWeight,
            decimal grossWeight,
            decimal width,
            decimal height,
            decimal length,
            decimal diameter,
            decimal area,
            decimal volume,
            WeightUnit weightUnit,
            LengthUnit lengthUnit,
            AreaUnit areaUnit,
            VolumeUnit volumeUnit,
            bool trackSerial,
            bool trackExpired,
            bool trackBatchNo,
            bool trackInventoryStatus,
            ItemCategory? itemCategory,
            Guid? itemGroupId,
            Guid? itemBrandId,
            Guid? itemGradeId,
            Guid? itemSizeId,
            Guid? colorPatternId,
            Guid? unitId,
            Guid? itemSeriesId,
            Guid? itemModelId,
            Guid? cpuId,
            Guid? ramId,
            Guid? vgaId,
            Guid? screenId,
            Guid? batteryId,
            Guid? cameraId,
            Guid? hddId,
            Guid? fieldAId,
            Guid? fieldBId,
            Guid? fieldCId,
            Guid? inventoryAccountId,
            Guid? purchaseAccountId,
            Guid? saleAccountId,
            Guid? purchaseTaxId,
            Guid saleTaxId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            ItemType = itemType;
            Code = code;
            Name = name;
            DisplayName = displayName;
            Description = description;
            ReorderStock = reorderStock;
            MinStock = minStock;
            MaxStock = maxStock;
            NetWeight = netWeight;
            GrossWeight = grossWeight;
            Width = width;
            Height = height;
            Length = length;
            Diameter = diameter;
            Area = area;
            Volume = volume;
            WeightUnit = weightUnit;
            LengthUnit = lengthUnit;
            AreaUnit = areaUnit;
            VolumeUnit = volumeUnit;
            TrackSerial = trackSerial;
            TrackExpired = trackExpired;
            TrackBatchNo = trackBatchNo;
            TrackInventoryStatus = trackInventoryStatus;
            ItemCategory = itemCategory;
            ItemGroupId = itemGroupId;
            ItemBrandId = itemBrandId;
            ItemGradeId = itemGradeId;
            ItemSizeId = itemSizeId;
            ColorPatternId = colorPatternId;
            UnitId = unitId;
            ItemSeriesId = itemSeriesId;
            ItemModelId = itemModelId;
            CPUId = cpuId;
            RAMId = ramId;
            VGAId = vgaId;
            ScreenId = screenId;
            BatteryId = batteryId;
            CameraId = cameraId;
            HDDId = hddId;
            FieldAId = fieldAId;
            FieldBId = fieldBId;
            FieldCId = fieldCId;
            InventoryAccountId = inventoryAccountId;
            PurchaseAccountId = purchaseAccountId;
            SaleAccountId = saleAccountId;
            PurchaseTaxId = purchaseTaxId;
            SaleTaxId = saleTaxId;
        }

    }
}
