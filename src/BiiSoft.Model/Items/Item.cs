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
    public enum WeightUnit
    {
        Kg = 0,
        Hg = 1,
        Dag = 2,
        g = 3,
        dg = 4,
        cg = 5,
        mg = 6,
        T = 7
    }

    public enum LengthUnit
    {
        Km = 0,
        Hm = 1,
        Dam = 2,
        m = 3,
        dm = 4,
        cm = 5,
        mm = 6,
    }

    public enum CubeUnit
    {
        Km3 = 0,
        Hm3 = 1,
        Dam3 = 2,
        m3 = 3,
        dm3 = 4,
        cm3 = 5,
        mm3 = 6,
    }

    public enum SquareUnit
    {
        Km2 = 0,
        Hm2 = 1,
        Dam2 = 2,
        m2 = 3,
        dm2 = 4,
        cm2 = 5,
        mm2 = 6,
    }

    public enum VolumeUnit
    {
        Kl = 0,
        Hl = 1,
        Dal = 2,
        l = 3,
        dl = 4,
        cl = 5,
        ml = 6,
    }


    [Table("BiiItems")]
    public class Item : NameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public ItemType ItemType { get; private set; }
        public ItemCategory? ItemCategory { get; private set; }
        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        [StringLength(BiiSoftConsts.MaxLengthLongCode, ErrorMessage = BiiSoftConsts.MaxLengthLongCodeErrorMessage)]
        public string Code { get; private set; }

        [Precision(18, 6)]
        public decimal NetWeight { get; private set; }
        [Precision(18, 6)]
        public decimal GrossWeight { get; private set; }

        [Precision(18, 6)]
        public decimal Volume { get; private set; }
       
        [Precision(18, 6)]
        public decimal Length { get; private set; }
        [Precision(18, 6)]
        public decimal Width { get; private set; }
        [Precision(18, 6)]
        public decimal Height { get; private set; }

        [Precision(18, 6)]
        public decimal Square { get; private set; }
        [Precision(18, 6)]
        public decimal Cube { get; private set; }

        public bool TrackSerial { get; private set; }
        public bool TrackExpired { get; private set; }
        public bool TrackStatus { get; private set; }

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
        public Guid? StorageId { get; private set; }
        public Storage Storage { get; private set; }
        public Guid? BatteryId { get; private set; }
        public Battery Battery { get; private set; }
        public Guid? CameraId { get; private set; }
        public Camera Camera { get; private set; }
        public Guid? ScreenId { get; private set; }
        public Screen Screen { get; private set; }
        public Guid? OSId { get; private set; }
        public OS OS { get; private set; }
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

        [Precision(18, 6)]
        public decimal StockAlert { get; private set; }
        [Precision(18, 6)]
        public decimal MinStock { get; private set; }
        [Precision(18, 6)]
        public decimal MaxStock { get; private set; }

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
            Guid? itemGroupId,
            Guid? unitId,
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
                Code = code,
                Name = name,
                DisplayName = displayName,
                Description = description,
                ItemCategory = itemCategory,
                ItemGroupId = itemGroupId,
                UnitId = unitId,
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
            ItemCategory? itemCategory,
            string code,
            string name,
            string displayName,
            string description,
            Guid? itemGroupId,
            Guid? unitId,
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
            ItemCategory = ItemCategory;
            ItemGroupId = itemGroupId;
            UnitId = unitId;
            PurchaseAccountId = purchaseAccountId;
            SaleAccountId = saleAccountId;
            PurchaseTaxId = purchaseTaxId;
            SaleTaxId = saleTaxId;
            IsActive = true;
        }

        public static Item Create(
            int tenantId,
            long userId,
            ItemType itemType,
            ItemCategory? itemCategory,
            string code,
            string name,
            string displayName,
            string description,
            decimal stockAlert,
            decimal minStock,
            decimal maxStock,
            decimal netWeight,
            decimal grossWeight,
            bool trackSerial,
            bool trackExpired,
            bool trackStatus,
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
            Guid? storageId,
            Guid? osId,
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
                Code = code,
                Name = name,
                DisplayName = displayName,
                Description = description,
                StockAlert = stockAlert,
                MinStock = minStock,
                MaxStock = maxStock,
                NetWeight = netWeight,
                GrossWeight = grossWeight,
                TrackSerial = trackSerial,
                TrackExpired = trackExpired,
                TrackStatus = trackStatus,
                ItemCategory = itemCategory,
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
                StorageId = storageId,
                OSId = osId,
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
            decimal stockAlert,
            decimal minStock,
            decimal maxStock,
            decimal netWeight,
            decimal grossWeight,
            bool trackSerial,
            bool trackExpired,
            bool trackStatus,
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
            Guid? storageId,
            Guid? osId,
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
            StockAlert = stockAlert;
            MinStock = minStock;
            MaxStock = maxStock;
            NetWeight = netWeight;
            GrossWeight = grossWeight;
            TrackSerial = trackSerial;
            TrackExpired = trackExpired;
            TrackStatus = trackStatus;
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
            StorageId = storageId;
            OSId = osId;
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
