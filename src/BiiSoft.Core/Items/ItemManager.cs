using Abp.Application.Features;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using BiiSoft.BFiles.Dto;
using BiiSoft.ChartOfAccounts;
using BiiSoft.Columns;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using BiiSoft.Features;
using BiiSoft.FileStorages;
using BiiSoft.Warehouses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Abp.Configuration;
using static Dapper.SqlMapper;
using Abp.Application.Services.Dto;

namespace BiiSoft.Items
{
    public class ItemManager : BiiSoftNameActiveValidateServiceBase<Item, Guid>, IItemManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        private readonly IBiiSoftRepository<ItemGroup, Guid> _itemGroupRepository;
        private readonly IBiiSoftRepository<ItemBrand, Guid> _itemBrandRepository;
        private readonly IBiiSoftRepository<ItemGrade, Guid> _itemGradeRepository;
        private readonly IBiiSoftRepository<ItemModel, Guid> _itemModelRepository;
        private readonly IBiiSoftRepository<ItemSize, Guid> _itemSizeRepository;
        private readonly IBiiSoftRepository<ItemSeries, Guid> _itemSeriesRepository;
        private readonly IBiiSoftRepository<ColorPattern, Guid> _colorPatternRepository;
        private readonly IBiiSoftRepository<CPU, Guid> _cpuRepository;
        private readonly IBiiSoftRepository<RAM, Guid> _ramRepository;
        private readonly IBiiSoftRepository<VGA, Guid> _vgaRepository;
        private readonly IBiiSoftRepository<HDD, Guid> _hddRepository;
        private readonly IBiiSoftRepository<Screen, Guid> _screenRepository;
        private readonly IBiiSoftRepository<Camera, Guid> _cameraRepository;
        private readonly IBiiSoftRepository<Battery, Guid> _batteryRepository;
        private readonly IBiiSoftRepository<FieldA, Guid> _fieldARepository;
        private readonly IBiiSoftRepository<FieldB, Guid> _fieldBRepository;
        private readonly IBiiSoftRepository<FieldC, Guid> _fieldCRepository;
        private readonly IBiiSoftRepository<Unit, Guid> _unitRepository;
        private readonly IBiiSoftRepository<ChartOfAccount, Guid> _chartOfAccountRepository;
        private readonly IBiiSoftRepository<ItemSetting, Guid> _itemSettingRepository;
        private readonly IBiiSoftRepository<ItemCodeFormula, Guid> _itemCodeFormulaRepository;
        private readonly IBiiSoftRepository<ItemZone, Guid> _itemZoneRepository;
        private readonly IBiiSoftRepository<Zone, Guid> _zoneRepository;
        private readonly IFeatureChecker _featureChecker;

        public ItemManager(
            IFeatureChecker featureChecker,
            IExcelManager excelManager,
            IBiiSoftRepository<Item, Guid> repository,
            IBiiSoftRepository<ItemGroup, Guid> itemGroupRepository,
            IBiiSoftRepository<ItemBrand, Guid> itemBrandRepository,
            IBiiSoftRepository<ItemGrade, Guid> itemGradeRepository,
            IBiiSoftRepository<ItemModel, Guid> itemModelRepository,
            IBiiSoftRepository<ItemSize, Guid> itemSizeRepository,
            IBiiSoftRepository<ItemSeries, Guid> itemSeriesRepository,
            IBiiSoftRepository<ColorPattern, Guid> colorPatternRepository,
            IBiiSoftRepository<CPU, Guid> cpuRepository,
            IBiiSoftRepository<RAM, Guid> ramRepository,
            IBiiSoftRepository<VGA, Guid> vgaRepository,
            IBiiSoftRepository<HDD, Guid> hddRepository,
            IBiiSoftRepository<Screen, Guid> screenRepository,
            IBiiSoftRepository<Camera, Guid> cameraRepository,
            IBiiSoftRepository<Battery, Guid> batteryRepository,
            IBiiSoftRepository<FieldA, Guid> fieldARepository,
            IBiiSoftRepository<FieldB, Guid> fieldBRepository,
            IBiiSoftRepository<FieldC, Guid> fieldCRepository,
            IBiiSoftRepository<Unit, Guid> unitRepository,
            IBiiSoftRepository<ChartOfAccount, Guid> chartOfAccountRepository,
            IBiiSoftRepository<ItemSetting, Guid> itemSettingRepository,
            IBiiSoftRepository<ItemCodeFormula, Guid> itemCodeFormulaRepository,
            IBiiSoftRepository<ItemZone, Guid> itemZoneRepository,
            IBiiSoftRepository<Zone, Guid> zoneRepository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager) : base(repository)
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _featureChecker = featureChecker;
            _excelManager = excelManager;
            _itemGroupRepository = itemGroupRepository;
            _itemBrandRepository = itemBrandRepository;
            _itemGradeRepository = itemGradeRepository;
            _itemModelRepository = itemModelRepository;
            _itemSizeRepository = itemSizeRepository;
            _itemSeriesRepository = itemSeriesRepository;
            _colorPatternRepository = colorPatternRepository;
            _cpuRepository = cpuRepository;
            _ramRepository = ramRepository;
            _vgaRepository = vgaRepository;
            _hddRepository = hddRepository;
            _screenRepository = screenRepository;
            _cameraRepository = cameraRepository;
            _batteryRepository = batteryRepository;
            _fieldARepository = fieldARepository;
            _fieldBRepository = fieldBRepository;
            _fieldCRepository = fieldCRepository;
            _unitRepository = unitRepository;
            _chartOfAccountRepository = chartOfAccountRepository;
            _itemSettingRepository = itemSettingRepository;
            _itemCodeFormulaRepository = itemCodeFormulaRepository;
            _itemZoneRepository = itemZoneRepository;
            _zoneRepository = zoneRepository;
        }

        #region override base class

        protected override string InstanceName => L("Item");

        protected override void ValidateInput(Item input)
        {
            ValidateCodeInput(input.Code);
            base.ValidateInput(input);

            ValidateSelect(input.UnitId, L("Unit"));
            ValidateSelect(input.PurchaseAccountId, L("PurchaseAccount"));
            ValidateSelect(input.SaleAccountId, L("SaleAccount"));

            if (input.ItemType == ItemType.Inventory ||
                input.ItemType == ItemType.Asset)
            {
                ValidateSelect(input.InventoryAccountId, L("InventoryAccount"));
            }

            if (!input.ItemZones.IsNullOrEmpty())
            {
                var checkDuplicate = input.ItemZones.GroupBy(s => s.ZoneId).Any(s => s.Count() > 1);
                if (checkDuplicate) DuplicateException(L("Zone"));
            }
        }

        protected override async Task ValidateInputAsync(Item input)
        {
            await base.ValidateInputAsync(input);

            var findCode = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Code == input.Code && s.Id != input.Id);
            if (findCode) DuplicateCodeException(input.Code);

            if (!input.Barcode.IsNullOrEmpty())
            {
                var findBarode = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Barcode == input.Barcode && s.Id != input.Id);
                if (findBarode) DuplicateException(L("Barcode"), input.Barcode);
            }

            var findUnit = await _unitRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.UnitId);
            if (!findUnit) InvalidException(L("Unit"));

            var itemSetting = await GetItemSettingAsync();

            if (itemSetting != null && itemSetting.UseItemGroup)
            {
                var find = await _itemGroupRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemGroupId);
                if (!find) InvalidException(L("ItemGroup"));
            }
            if (itemSetting != null && itemSetting.UseBrand)
            {
                var find = await _itemBrandRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemBrandId);
                if (!find) InvalidException(L("ItemBrand"));
            }
            if (itemSetting != null && itemSetting.UseGrade)
            {
                var find = await _itemGradeRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemGradeId);
                if (!find) InvalidException(L("ItemGrade"));
            }
            if (itemSetting != null && itemSetting.UseModel)
            {
                var find = await _itemModelRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemModelId);
                if (!find) InvalidException(L("ItemModel"));
            }
            if (itemSetting != null && itemSetting.UseSize)
            {
                var find = await _itemSizeRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemSizeId);
                if (!find) InvalidException(L("ItemSize"));
            }
            if (itemSetting != null && itemSetting.UseSeries)
            {
                var find = await _itemSeriesRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemSeriesId);
                if (!find) InvalidException(L("ItemSeries"));
            }
            if (itemSetting != null && itemSetting.UseColorPattern)
            {
                var find = await _colorPatternRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ColorPatternId);
                if (!find) InvalidException(L("ColorPattern"));
            }
            if (itemSetting != null && itemSetting.UseCPU)
            {
                var find = await _cpuRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CPUId);
                if (!find) InvalidException(L("CPU"));
            }
            if (itemSetting != null && itemSetting.UseRAM)
            {
                var find = await _ramRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.RAMId);
                if (!find) InvalidException(L("RAM"));
            }
            if (itemSetting != null && itemSetting.UseVGA)
            {
                var find = await _vgaRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.VGAId);
                if (!find) InvalidException(L("VGA"));
            }
            if (itemSetting != null && itemSetting.UseHDD)
            {
                var find = await _hddRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.HDDId);
                if (!find) InvalidException(L("HDD"));
            }
            if (itemSetting != null && itemSetting.UseScreen)
            {
                var find = await _screenRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ScreenId);
                if (!find) InvalidException(L("Screen"));
            }
            if (itemSetting != null && itemSetting.UseCamera)
            {
                var find = await _cameraRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CameraId);
                if (!find) InvalidException(L("Camera"));
            }
            if (itemSetting != null && itemSetting.UseBattery)
            {
                var find = await _batteryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.BatteryId);
                if (!find) InvalidException(L("Battery"));
            }
            if (itemSetting != null && itemSetting.UseFieldA)
            {
                var find = await _fieldARepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.FieldAId);
                if (!find) InvalidException(L("FieldA"));
            }
            if (itemSetting != null && itemSetting.UseFieldB)
            {
                var find = await _fieldBRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.FieldBId);
                if (!find) InvalidException(L("FieldB"));
            }
            if (itemSetting != null && itemSetting.UseFieldC)
            {
                var find = await _fieldCRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.FieldCId);
                if (!find) InvalidException(L("FieldC"));
            }

            if (!input.PurchaseAccountId.IsNullOrEmpty())
            {
                var find = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.PurchaseAccountId);
                if (!find) InvalidException(L("PurchaseAccount"));
            }
            if (!input.SaleAccountId.IsNullOrEmpty())
            {
                var find = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.SaleAccountId);
                if (!find) InvalidException(L("SaleAccount"));
            }
            if (input.ItemType == ItemType.Inventory ||
                input.ItemType == ItemType.Asset)
            {
                var find = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.InventoryAccountId);
                if (!find) InvalidException(input.ItemType == ItemType.NonInventory ? L("InventoryAccount") : L("AssetAccount"));
            }

            if (!input.ItemZones.IsNullOrEmpty())
            {
                var validZone = await _zoneRepository.GetAll().AsNoTracking().Where(s => input.ItemZones.Any(r => r.ZoneId == s.Id)).CountAsync() == input.ItemZones.Count;

                if (!validZone) InvalidException(L("Zone"));
            }
        }

        protected override Item CreateInstance(Item input)
        {
            var entity = Item.Create(
                input.TenantId,
                input.CreatorUserId.Value,
                input.ItemType,
                input.ItemCategory,
                input.Code,
                input.Barcode,
                input.Name,
                input.DisplayName,
                input.Description,
                input.ReorderStock,
                input.MinStock,
                input.MaxStock,
                input.NetWeight,
                input.GrossWeight,
                input.Width,
                input.Height,
                input.Length,
                input.Diameter,
                input.Area,
                input.Volume,
                input.WeightUnit,
                input.LengthUnit,
                input.AreaUnit,
                input.VolumeUnit,
                input.TrackSerial,
                input.TrackExpired,
                input.TrackBatchNo,
                input.TrackAssetStatus,
                input.ItemGroupId,
                input.ItemBrandId,
                input.ItemGradeId,
                input.ItemSizeId,
                input.ColorPatternId,
                input.UnitId,
                input.ItemSeriesId,
                input.ItemModelId,
                input.CPUId,
                input.RAMId,
                input.VGAId,
                input.ScreenId,
                input.BatteryId,
                input.CameraId,
                input.HDDId,
                input.FieldAId,
                input.FieldBId,
                input.FieldCId,
                input.InventoryAccountId,
                input.PurchaseAccountId,
                input.SaleAccountId,
                input.IsModifier,
                input.IsAddOn,
                input.UseBOM,
                input.DisplayBOM,
                input.ALTCode);

            entity.SetImage(input.ImageId);

            return entity;
        }

        protected override void UpdateInstance(Item input, Item entity)
        {
            entity.Update(
                input.LastModifierUserId.Value,
                input.ItemType,
                input.ItemCategory,
                input.Code,
                input.Barcode,
                input.Name,
                input.DisplayName,
                input.Description,
                input.ReorderStock,
                input.MinStock,
                input.MaxStock,
                input.NetWeight,
                input.GrossWeight,
                input.Width,
                input.Height,
                input.Length,
                input.Diameter,
                input.Area,
                input.Volume,
                input.WeightUnit,
                input.LengthUnit,
                input.AreaUnit,
                input.VolumeUnit,
                input.TrackSerial,
                input.TrackExpired,
                input.TrackBatchNo,
                input.TrackAssetStatus,
                input.ItemGroupId,
                input.ItemBrandId,
                input.ItemGradeId,
                input.ItemSizeId,
                input.ColorPatternId,
                input.UnitId,
                input.ItemSeriesId,
                input.ItemModelId,
                input.CPUId,
                input.RAMId,
                input.VGAId,
                input.ScreenId,
                input.BatteryId,
                input.CameraId,
                input.HDDId,
                input.FieldAId,
                input.FieldBId,
                input.FieldCId,
                input.InventoryAccountId,
                input.PurchaseAccountId,
                input.SaleAccountId,
                input.IsModifier,
                input.IsAddOn,
                input.UseBOM,
                input.DisplayBOM,
                input.ALTCode);

            entity.SetImage(input.ImageId);
        }

        #endregion

        private async Task<ItemSetting> GetItemSettingAsync()
        {
            return await _itemSettingRepository.GetAll().AsNoTracking().FirstOrDefaultAsync();
        }

        private async Task SetCodeAsync(Item input)
        {
            if (!input.Code.IsNullOrWhiteSpace()) return;

            var itemSetting = await GetItemSettingAsync();

            if (itemSetting == null || !itemSetting.UseCodeFormula) return;

            var formula = await _itemCodeFormulaRepository.GetAll()
                                .Include(s => s.ItemTypes)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(s => s.IsAllItemType || s.ItemTypes.Any(r => r.ItemType == input.ItemType));

            if (formula == null || formula.Type == ItemCodeFormulaType.Manual) return;

            var prefix = "";
            if (formula.Type == ItemCodeFormulaType.Custom)
            {
                prefix = formula.Prefix;
            }

            var latestCode = await _repository.GetAll()
                            .AsNoTracking()
                            .Where(s => formula.ItemTypes.Any(r => r.ItemType == s.ItemType))
                            .Where(s => s.Code.StartsWith(prefix))
                            .Select(s => s.Code)
                            .OrderByDescending(s => s)
                            .FirstOrDefaultAsync();

            if (latestCode.IsNullOrWhiteSpace())
            {
                input.SetCode(formula.Start.GenerateCode(formula.Digits, prefix));
            }
            else
            {
                input.SetCode(latestCode.NextCode(prefix));
            }
        }

        protected override async Task BeforeInstanceUpdateAsync(Item input, Item entity)
        {
            var itemZones = await _itemZoneRepository.GetAll().AsNoTracking().Where(s => s.ItemId == input.Id).ToListAsync();

            var addZones = new List<ItemZone>();
            var updateZones = new List<ItemZone>();

            foreach (var zone in input.ItemZones)
            {
                var updateZone = itemZones.FirstOrDefault(s => s.ZoneId == zone.ZoneId);
                if (updateZone != null)
                {
                    updateZones.Add(updateZone);
                }
                else
                {
                    addZones.Add(ItemZone.Create(input.TenantId, input.CreatorUserId.Value, input.Id, zone.ZoneId));
                }
            }

            if (addZones.Any()) await _itemZoneRepository.BulkInsertAsync(addZones);

            var deleteZones = itemZones.Where(s => !updateZones.Any(r => r.ZoneId == s.ZoneId)).ToList();
            if (deleteZones.Any()) await _itemZoneRepository.BulkDeleteAsync(deleteZones);
        }

        public override async Task<IdentityResult> InsertAsync(Item input)
        {
            await SetCodeAsync(input);
            var result = await base.InsertAsync(input);

            if (!input.ItemZones.IsNullOrEmpty())
            {
                var addZones = input.ItemZones.Select(s => ItemZone.Create(input.TenantId, input.CreatorUserId.Value, input.Id, s.ZoneId)).ToList();
                await CurrentUnitOfWork.SaveChangesAsync();
                await _itemZoneRepository.BulkInsertAsync(addZones);
            }

            return result;
        }

        private bool AccountingFeatureEnable => _featureChecker.IsEnabled(AppFeatures.Accounting_ChartOfAccounts);

        private async Task<List<ColumnOutput>> GetExcelTemplateColumnsAsync()
        {
            var setting = await GetItemSettingAsync();

            if (setting == null) RequiredException(L("ItemSetting"));

            var columns = new List<ColumnOutput>
            {
                new ColumnOutput{ ColumnName = "Name",  ColumnTitle = L("Name_",L("Item")), Width = 250, Index = 1, IsRequired = true },
                new ColumnOutput{ ColumnName = "DisplayName", ColumnTitle = L("DisplayName"), Width = 250, Index = 2, IsRequired = true },
                new ColumnOutput{ ColumnName = "ItemType", ColumnTitle = L("ItemType"), Width = 150, Index = 3, IsRequired = true, ColumnType = ColumnType.Lookup, LookupList = ItemType.Service.ToListStr() },
                new ColumnOutput{ ColumnName = "ItemCategory",  ColumnTitle = L("ItemCategory"), Width = 150, Index = 4, IsRequired = true, ColumnType = ColumnType.Lookup, LookupList = ItemCategory.Service.ToListStr() },
                new ColumnOutput{ ColumnName = "Code", ColumnTitle = L("Code"), Width = 100, Index = 5, IsRequired = setting.UseCodeFormula },
                new ColumnOutput{ ColumnName = "Barcode", ColumnTitle = L("Barcode"), Width = 150, Index = 6 },
                new ColumnOutput{ ColumnName = "ALTCode",  ColumnTitle = L("ALTCode"), Width = 100, Index = 7 },
                new ColumnOutput{ ColumnName = "Description",  ColumnTitle = L("Description"), Width = 150, Index = 8 },
                new ColumnOutput { ColumnName = "Unit", ColumnTitle = L("PackageUnit"), Width = 150, Index = 9, IsRequired = true }
            };

            var index = columns.Select(s => s.Index).OrderByDescending(s => s).FirstOrDefault() + 1;

            if (setting.UseGrossWeight)
            {
                columns.Add(new ColumnOutput { ColumnName = "GrossWeight", ColumnTitle = L("GrossWeight"), Width = 100, Index = index, IsRequired = setting.GrossWeightRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseNetWeight)
            {
                columns.Add(new ColumnOutput { ColumnName = "NetWeight", ColumnTitle = L("NetWeight"), Width = 100, Index = index, IsRequired = setting.NetWeightRequired, ColumnType = ColumnType.Number });
                index++;
            }


            if (setting.UseGrossWeight || setting.UseNetWeight)
            {
                columns.Add(new ColumnOutput { ColumnName = "WeightUnit", ColumnTitle = L("WeightUnit"), Width = 100, Index = index, IsRequired = setting.GrossWeightRequired || setting.NetWeightRequired, ColumnType = ColumnType.Lookup, LookupList = WeightUnit.g.ToListStr() });
                index++;
            }

            if (setting.UseWidth)
            {
                columns.Add(new ColumnOutput { ColumnName = "Width", ColumnTitle = L("Width"), Width = 100, Index = index, IsRequired = setting.WidthRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseHeight)
            {
                columns.Add(new ColumnOutput { ColumnName = "Height", ColumnTitle = L("Height"), Width = 100, Index = index, IsRequired = setting.HeightRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseLength)
            {
                columns.Add(new ColumnOutput { ColumnName = "Length", ColumnTitle = L("Length"), Width = 100, Index = index, IsRequired = setting.LengthRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseDiameter)
            {
                columns.Add(new ColumnOutput { ColumnName = "Diameter", ColumnTitle = L("Diameter"), Width = 100, Index = index, IsRequired = setting.DiameterRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseWidth || setting.UseHeight || setting.UseLength || setting.UseDiameter)
            {
                columns.Add(new ColumnOutput { ColumnName = "LengthUnit", ColumnTitle = L("LengthUnit"), Width = 100, Index = index, IsRequired = setting.WidthRequired || setting.HeightRequired || setting.LengthRequired || setting.DiameterRequired, ColumnType = ColumnType.Lookup, LookupList = LengthUnit.m.ToListStr() });
                index++;
            }

            if (setting.UseArea)
            {
                columns.Add(new ColumnOutput { ColumnName = "Area", ColumnTitle = L("Area"), Width = 100, Index = index, IsRequired = setting.AreaRequired, ColumnType = ColumnType.Number });
                columns.Add(new ColumnOutput { ColumnName = "AreaUnit", ColumnTitle = L("AreaUnit"), Width = 100, Index = index + 1, IsRequired = setting.AreaRequired, ColumnType = ColumnType.Lookup, LookupList = AreaUnit.m2.ToListStr() });
                index += 2;
            }

            if (setting.UseVolume)
            {
                columns.Add(new ColumnOutput { ColumnName = "Volume", ColumnTitle = L("Volume"), Width = 100, Index = index, IsRequired = setting.VolumeRequired, ColumnType = ColumnType.Number });
                columns.Add(new ColumnOutput { ColumnName = "VolumeUnit", ColumnTitle = L("VolumeUnit"), Width = 100, Index = index + 1, IsRequired = setting.VolumeRequired, ColumnType = ColumnType.Lookup, LookupList = VolumeUnit.m3.ToListStr() });
                index += 2;
            }

            if (AccountingFeatureEnable)
            {
                columns.AddRange(new List<ColumnOutput> {
                    new ColumnOutput {ColumnName = "PurchaseAccount",  ColumnTitle = L("PurchaseAccount"), Width = 150, Index = index, IsRequired = true },
                    new ColumnOutput {ColumnName = "SaleAccount",  ColumnTitle = L("SaleAccount"), Width = 150, Index = index + 1, IsRequired = true },
                    new ColumnOutput {ColumnName = "InventoryAccount", ColumnTitle = L("InventoryAccount"), Width = 150, Index = index + 2},
                });

                index += 3;
            }

            if (setting.UseItemGroup)
            {
                columns.Add(new ColumnOutput { ColumnName = "ItemGroup", ColumnTitle = L("ItemGroup"), Width = 150, Index = index, IsRequired = setting.ItemGroupRequired });
                index++;
            }

            if (setting.UseBrand)
            {
                columns.Add(new ColumnOutput { ColumnName = "ItemBrand", ColumnTitle = L("ItemBrand"), Width = 150, Index = index, IsRequired = setting.BrandRequired });
                index++;
            }

            if (setting.UseGrade)
            {
                columns.Add(new ColumnOutput { ColumnName = "ItemGrade", ColumnTitle = L("ItemGrade"), Width = 150, Index = index, IsRequired = setting.GradeRequired });
                index++;
            }

            if (setting.UseModel)
            {
                columns.Add(new ColumnOutput { ColumnName = "ItemModel", ColumnTitle = L("ItemModel"), Width = 150, Index = index, IsRequired = setting.ModelRequired });
                index++;
            }

            if (setting.UseSize)
            {
                columns.Add(new ColumnOutput { ColumnName = "ItemSize", ColumnTitle = L("ItemSize"), Width = 150, Index = index, IsRequired = setting.SizeRequired });
                index++;
            }

            if (setting.UseSeries)
            {
                columns.Add(new ColumnOutput { ColumnName = "ItemSeries", ColumnTitle = L("ItemSeries"), Width = 150, Index = index, IsRequired = setting.SeriesRequired });
                index++;
            }

            if (setting.UseColorPattern)
            {
                columns.Add(new ColumnOutput { ColumnName = "ColorPattern", ColumnTitle = L("ColorPattern"), Width = 150, Index = index, IsRequired = setting.ColorPatternRequired });
                index++;
            }

            if (setting.UseCPU)
            {
                columns.Add(new ColumnOutput { ColumnName = "CPU", ColumnTitle = L("CPU"), Width = 150, Index = index, IsRequired = setting.CPURequired });
                index++;
            }

            if (setting.UseRAM)
            {
                columns.Add(new ColumnOutput { ColumnName = "RAM", ColumnTitle = L("RAM"), Width = 150, Index = index, IsRequired = setting.RAMRequired });
                index++;
            }

            if (setting.UseVGA)
            {
                columns.Add(new ColumnOutput { ColumnName = "VGA", ColumnTitle = L("VGA"), Width = 150, Index = index, IsRequired = setting.VGARequired });
                index++;
            }

            if (setting.UseHDD)
            {
                columns.Add(new ColumnOutput { ColumnName = "HDD", ColumnTitle = L("HDD"), Width = 150, Index = index, IsRequired = setting.HDDRequired });
                index++;
            }

            if (setting.UseScreen)
            {
                columns.Add(new ColumnOutput { ColumnName = "Screen", ColumnTitle = L("Screen"), Width = 150, Index = index, IsRequired = setting.ScreenRequired });
                index++;
            }

            if (setting.UseCamera)
            {
                columns.Add(new ColumnOutput { ColumnName = "Camera", ColumnTitle = L("Camera"), Width = 150, Index = index, IsRequired = setting.CameraRequired });
                index++;
            }

            if (setting.UseBattery)
            {
                columns.Add(new ColumnOutput { ColumnName = "Battery", ColumnTitle = L("Battery"), Width = 150, Index = index, IsRequired = setting.BatteryRequired });
                index++;
            }

            if (setting.UseFieldA)
            {
                columns.Add(new ColumnOutput { ColumnName = "FieldA", ColumnTitle = setting.FieldALabel.IsNullOrEmpty() ? L("FieldA") : L(setting.FieldALabel), Width = 150, Index = index, IsRequired = setting.FieldARequired });
                index++;
            }

            if (setting.UseFieldB)
            {
                columns.Add(new ColumnOutput { ColumnName = "FieldB", ColumnTitle = setting.FieldBLabel.IsNullOrEmpty() ? L("FieldB") : L(setting.FieldBLabel), Width = 150, Index = index, IsRequired = setting.FieldBRequired });
                index++;
            }

            if (setting.UseFieldC)
            {
                columns.Add(new ColumnOutput { ColumnName = "FieldC", ColumnTitle = setting.FieldBLabel.IsNullOrEmpty() ? L("FieldC") : L(setting.FieldCLabel), Width = 150, Index = index, IsRequired = setting.FieldCRequired });
                index++;
            }

            if (setting.UseSerial)
            {
                columns.Add(new ColumnOutput { ColumnName = "TrackSerial", ColumnTitle = L("TrackSerial"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
                index++;
            }

            if (setting.UseExpired)
            {
                columns.Add(new ColumnOutput { ColumnName = "TrackExpired", ColumnTitle = L("TrackExpired"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
                index++;
            }

            if (setting.UseBatchNo)
            {
                columns.Add(new ColumnOutput { ColumnName = "TrackBatchNo", ColumnTitle = L("TrackBatchNo"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
                index++;
            }

            if (setting.UseAssetStatus)
            {
                columns.Add(new ColumnOutput { ColumnName = "TrackAssetStatus", ColumnTitle = L("TrackAssetStatus"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
                index++;
            }

            if (setting.UseReorderStock)
            {
                columns.Add(new ColumnOutput { ColumnName = "ReorderStock", ColumnTitle = L("ReorderStock"), Width = 100, Index = index, IsRequired = setting.ReorderStockRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseMaxStock)
            {
                columns.Add(new ColumnOutput { ColumnName = "MaxStock", ColumnTitle = L("MaxStock"), Width = 100, Index = index, IsRequired = setting.MaxStockRequired, ColumnType = ColumnType.Number });
                index++;
            }

            if (setting.UseMinStock)
            {
                columns.Add(new ColumnOutput { ColumnName = "MinStock", ColumnTitle = L("MinStock"), Width = 100, Index = index, IsRequired = setting.MinStockRequired, ColumnType = ColumnType.Number });
                index++;
            }

            columns.Add(new ColumnOutput { ColumnName = "IsModifier", ColumnTitle = L("IsModifier"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
            index++;

            columns.Add(new ColumnOutput { ColumnName = "IsAddOn", ColumnTitle = L("IsAddOn"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
            index++;

            columns.Add(new ColumnOutput { ColumnName = "UseBOM", ColumnTitle = L("UseBOM"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
            index++;

            columns.Add(new ColumnOutput { ColumnName = "DisplayBOM", ColumnTitle = L("DisplayBOM"), Width = 100, Index = index, ColumnType = ColumnType.Bool });
            index++;


            return columns;
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"Item.xlsx",
                Columns = await GetExcelTemplateColumnsAsync()
            };

            return await _excelManager.ExportExcelTemplateAsync(fileInput);
        }

        /// <summary>
        /// Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<Guid> input)
        {
            var itemCodeFormulas = new List<ItemCodeFormula>();
            ItemSetting itemSetting = null;

            var barcodeHash = new HashSet<string>();
            var altCodeHash = new HashSet<string>();
            var itemDic = new Dictionary<string, ItemType>();
            var unitDic = new Dictionary<string, Guid>();
            var itemGroupDic = new Dictionary<string, Guid>();
            var itemBrandDic = new Dictionary<string, Guid>();
            var itemModelDic = new Dictionary<string, Guid>();
            var itemGradeDic = new Dictionary<string, Guid>();
            var itemSizeDic = new Dictionary<string, Guid>();
            var itemSeriesDic = new Dictionary<string, Guid>();
            var colorPatternDic = new Dictionary<string, Guid>();
            var cpuDic = new Dictionary<string, Guid>();
            var ramDic = new Dictionary<string, Guid>();
            var vgaDic = new Dictionary<string, Guid>();
            var hddDic = new Dictionary<string, Guid>();
            var cameraDic = new Dictionary<string, Guid>();
            var batteryDic = new Dictionary<string, Guid>();
            var screenDic = new Dictionary<string, Guid>();
            var fieldADic = new Dictionary<string, Guid>();
            var fieldBDic = new Dictionary<string, Guid>();
            var fieldCDic = new Dictionary<string, Guid>();
            var accountDic = new Dictionary<string, KeyValuePair<Guid, AccountType>>();

            var columns = new List<ColumnOutput>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    itemCodeFormulas = await _itemCodeFormulaRepository.GetAll().Include(s => s.ItemTypes).AsNoTracking().ToListAsync();
                    itemSetting = await GetItemSettingAsync();

                    if (itemSetting == null) InputException(L("ItemSetting"));

                    var items = await _repository.GetAll().AsNoTracking()
                                      .Select(s => new
                                      {
                                          s.Code,
                                          s.ItemType,
                                          s.Barcode,
                                          s.ALTCode
                                      })
                                      .OrderByDescending(s => s.Code)
                                      .ToListAsync();

                    itemDic = items.ToDictionary(k => k.Code, v => v.ItemType);
                    barcodeHash = items.Where(s => !s.Barcode.IsNullOrEmpty()).Select(s => s.Barcode).ToHashSet(StringComparer.OrdinalIgnoreCase);
                    altCodeHash = items.Where(s => !s.ALTCode.IsNullOrEmpty()).Select(s => s.ALTCode).ToHashSet(StringComparer.OrdinalIgnoreCase);

                    unitDic = await _unitRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    itemGroupDic = await _itemGroupRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    itemBrandDic = await _itemBrandRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    itemModelDic = await _itemModelRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    itemGradeDic = await _itemGradeRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    itemSizeDic = await _itemSizeRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    itemSeriesDic = await _itemSeriesRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    colorPatternDic = await _colorPatternRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    cpuDic = await _cpuRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    ramDic = await _ramRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    vgaDic = await _vgaRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    hddDic = await _hddRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    cameraDic = await _cameraRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    batteryDic = await _batteryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    screenDic = await _screenRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    fieldADic = await _fieldARepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    fieldBDic = await _fieldBRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    fieldCDic = await _fieldCRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
                    accountDic = await _chartOfAccountRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => new KeyValuePair<Guid, AccountType>(v.Id, v.AccountType));

                    columns = await GetExcelTemplateColumnsAsync();
                }
            }

            var addItems = new List<Item>();

            var excelPackage = await _fileStorageManager.DownloadExcel(input.Token);
            if (excelPackage != null)
            {
                // Get the work book in the file
                var workBook = excelPackage.Workbook;
                if (workBook != null)
                {
                    // retrive first worksheets
                    var worksheet = excelPackage.Workbook.Worksheets[0];
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {

                        ItemType itemType = ItemType.Service;
                        ItemCategory itemCategory = ItemCategory.Service;
                        string name = "";
                        string displayName = "";
                        string code = "";
                        string barcode = "";
                        string altCode = "";
                        string description = "";
                        decimal reorderStock = 0;
                        decimal minStock = 0;
                        decimal maxStock = 0;
                        decimal netWeight = 0;
                        decimal grossWeight = 0;
                        decimal width = 0;
                        decimal height = 0;
                        decimal length = 0;
                        decimal diameter = 0;
                        decimal area = 0;
                        decimal volume = 0;
                        WeightUnit? weightUnit = null;
                        LengthUnit? lengthUnit = null;
                        AreaUnit? areaUnit = null;
                        VolumeUnit? volumeUnit = null;
                        bool? trackSerial = null;
                        bool? trackExpired = null;
                        bool? trackBatchNo = null;
                        bool? trackAssetStatus = null;
                        Guid? itemGroupId = null;
                        Guid? itemBrandId = null;
                        Guid? itemGradeId = null;
                        Guid? itemModelId = null;
                        Guid? itemSizeId = null;
                        Guid? itemSeriesId = null;
                        Guid? colorPatternId = null;
                        Guid? unitId = null;
                        Guid? cpuId = null;
                        Guid? ramId = null;
                        Guid? vgaId = null;
                        Guid? screenId = null;
                        Guid? batteryId = null;
                        Guid? cameraId = null;
                        Guid? hddId = null;
                        Guid? fieldAId = null;
                        Guid? fieldBId = null;
                        Guid? fieldCId = null;
                        Guid? purchaseAccountId = null;
                        Guid? saleAccountId = null;
                        Guid? inventoryAccountId = null;
                        bool? isModifier = false;
                        bool? isAddOn = false;
                        bool? useBOM = false;
                        bool? displayBOM = false;

                        var rowMessage = $", Row: {i}";

                        foreach (var col in columns)
                        {
                            switch (col.ColumnName)
                            {
                                case "ItemType":
                                    var itemTypeName = worksheet.GetString(i, col.Index);
                                    ValidateSelect(itemTypeName, col.ColumnTitle, rowMessage);
                                    itemType = Enum.Parse<ItemType>(itemTypeName);
                                    break;
                                case "ItemCategory":
                                    var itemCategoryName = worksheet.GetString(i, col.Index);
                                    ValidateSelect(itemCategoryName, col.ColumnTitle, rowMessage);
                                    itemCategory = Enum.Parse<ItemCategory>(itemCategoryName);
                                    break;
                                case "Name":
                                    name = worksheet.GetString(i, col.Index);
                                    ValidateInput(name, col.ColumnTitle, rowMessage);
                                    break;
                                case "DisplayName":
                                    displayName = worksheet.GetString(i, col.Index);
                                    ValidateDisplayName(displayName, rowMessage);
                                    break;
                                case "Code":
                                    code = worksheet.GetString(i, col.Index);

                                    if (!itemSetting.UseCodeFormula)
                                    {
                                        ValidateCodeInput(code, rowMessage);
                                    }
                                    else
                                    {
                                        var formula = itemCodeFormulas.Where(s => s.IsAllItemType || s.ItemTypes.Any(r => r.ItemType == itemType)).FirstOrDefault();

                                        if (formula == null) InputException(L("ItemCodeFormula"), rowMessage);

                                        if (formula.Type == ItemCodeFormulaType.Manual)
                                        {
                                            ValidateCodeInput(code, rowMessage);
                                        }
                                        else if (code.IsNullOrEmpty())
                                        {
                                            var prefix = formula.Prefix;

                                            var latestCode = itemDic
                                                            .Where(s => formula.IsAllItemType || formula.ItemTypes.Any(r => r.ItemType == s.Value))
                                                            .Where(s => s.Key.StartsWith(prefix))
                                                            .Select(s => s.Key)
                                                            .OrderByDescending(s => s)
                                                            .FirstOrDefault();

                                            if (latestCode.IsNullOrWhiteSpace())
                                            {
                                                code = formula.Start.GenerateCode(formula.Digits, prefix);
                                            }
                                            else
                                            {
                                                code = latestCode.NextCode(prefix);
                                            }
                                        }
                                    }

                                    if (itemDic.ContainsKey(code) || altCodeHash.Contains(code)) DuplicateCodeException(code, rowMessage);

                                    break;
                                case "Barcode":
                                    barcode = worksheet.GetString(i, col.Index);
                                    if (!barcode.IsNullOrEmpty() && barcodeHash.Contains(barcode)) DuplicateException(col.ColumnTitle, rowMessage);
                                    break;
                                case "ALTCode":
                                    altCode = worksheet.GetString(i, col.Index);
                                    if (!altCode.IsNullOrEmpty())
                                    {
                                        if (altCode == code ||
                                           altCodeHash.Contains(altCode) ||
                                           itemDic.ContainsKey(altCode)) DuplicateException(col.ColumnTitle, rowMessage);
                                    }
                                    break;
                                case "Description":
                                    description = worksheet.GetString(i, col.Index);
                                    break;
                                case "Unit":
                                    var unitName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateSelect(unitName, col.ColumnTitle, rowMessage);
                                    if (!unitName.IsNullOrEmpty())
                                    {
                                        if (!unitDic.ContainsKey(unitName)) InvalidException(col.ColumnTitle, rowMessage);
                                        unitId = unitDic[unitName];
                                    }
                                    break;
                                case "ReorderStock":
                                    reorderStock = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && reorderStock == 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "MinStock":
                                    minStock = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && minStock < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "MaxStock":
                                    maxStock = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && maxStock < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "NetWeight":
                                    netWeight = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && netWeight < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "GrossWeight":
                                    grossWeight = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && grossWeight < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "Width":
                                    width = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && width < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "Height":
                                    height = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && height < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "Length":
                                    length = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && length < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "Diameter":
                                    diameter = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && diameter < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "Area":
                                    area = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && area < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "Volume":
                                    volume = worksheet.GetDecimal(i, col.Index);
                                    if (col.IsRequired && volume < 0) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "WeightUnit":
                                    var weightUnitName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateSelect(weightUnitName, col.ColumnTitle, rowMessage);
                                    if (!weightUnitName.IsNullOrEmpty())
                                    {
                                        if (!Enum.TryParse(weightUnitName, out WeightUnit weightUnitValue)) InvalidException(col.ColumnTitle, rowMessage);
                                        weightUnit = weightUnitValue;
                                    }
                                    break;
                                case "LengthUnit":
                                    var lengthUnitName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateSelect(lengthUnitName, col.ColumnTitle, rowMessage);
                                    if (!lengthUnitName.IsNullOrEmpty())
                                    {
                                        if (!Enum.TryParse(lengthUnitName, out LengthUnit lengthUnitValue)) InvalidException(col.ColumnTitle, rowMessage);
                                        lengthUnit = lengthUnitValue;
                                    }
                                    break;
                                case "AreaUnit":
                                    var areaUnitName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateSelect(areaUnitName, col.ColumnTitle, rowMessage);
                                    if (!areaUnitName.IsNullOrEmpty())
                                    {
                                        if (!Enum.TryParse(areaUnitName, out AreaUnit areaUnitValue)) InvalidException(col.ColumnTitle, rowMessage);
                                        areaUnit = areaUnitValue;
                                    }
                                    break;
                                case "VolumeUnit":
                                    var volumeUnitName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateSelect(volumeUnitName, col.ColumnTitle, rowMessage);
                                    if (!volumeUnitName.IsNullOrEmpty())
                                    {
                                        if (!Enum.TryParse(volumeUnitName, out VolumeUnit volumeUnitValue)) InvalidException(col.ColumnTitle, rowMessage);
                                        volumeUnit = volumeUnitValue;
                                    }
                                    break;
                                case "TrackSerial":
                                    trackSerial = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !trackSerial.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "TrackExpired":
                                    trackExpired = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !trackExpired.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "TrackBatchNo":
                                    trackBatchNo = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !trackBatchNo.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "TrackAssetStatus":
                                    trackAssetStatus = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !trackAssetStatus.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "PurchaseAccount":
                                    var purchaseAccountName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(purchaseAccountName, col.ColumnTitle, rowMessage);
                                    if (!purchaseAccountName.IsNullOrEmpty())
                                    {
                                        if (!accountDic.ContainsKey(purchaseAccountName)) InvalidException(col.ColumnTitle, rowMessage);
                                        if (!accountDic[purchaseAccountName].Value.IsExpenseOrCostOfSale()) InvalidException(col.ColumnTitle, rowMessage);
                                        purchaseAccountId = accountDic[purchaseAccountName].Key;
                                    }
                                    break;
                                case "SaleAccount":
                                    var saleAccountName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(saleAccountName, col.ColumnTitle, rowMessage);
                                    if (!saleAccountName.IsNullOrEmpty())
                                    {
                                        if (!accountDic.ContainsKey(saleAccountName)) InvalidException(col.ColumnTitle, rowMessage);
                                        if (!accountDic[saleAccountName].Value.IsRevenue()) InvalidException(col.ColumnTitle, rowMessage);
                                        saleAccountId = accountDic[saleAccountName].Key;
                                    }
                                    break;
                                case "InventoryAccount":
                                    var inventoryAccountName = worksheet.GetString(i, col.Index);
                                    if (itemType == ItemType.Inventory || itemType == ItemType.Asset) ValidateInput(inventoryAccountName, col.ColumnTitle, rowMessage);
                                    if (!inventoryAccountName.IsNullOrEmpty())
                                    {
                                        if (!accountDic.ContainsKey(inventoryAccountName)) InvalidException(col.ColumnTitle, rowMessage);
                                        if (itemType == ItemType.Asset && accountDic[inventoryAccountName].Value != AccountType.FixedAsset) InvalidException(L("AssetAccount"), rowMessage);
                                        if (itemType != ItemType.Inventory && accountDic[inventoryAccountName].Value != AccountType.Inventory) InvalidException(L("InventoryAccount"), rowMessage);
                                        inventoryAccountId = accountDic[inventoryAccountName].Key;
                                    }
                                    break;
                                case "ItemGroup":
                                    var itemGroupName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(itemGroupName, col.ColumnTitle, rowMessage);
                                    if (!itemGroupName.IsNullOrEmpty())
                                    {
                                        if (!itemGroupDic.ContainsKey(itemGroupName)) InvalidException(col.ColumnTitle, rowMessage);
                                        itemGroupId = itemGroupDic[itemGroupName];
                                    }
                                    break;
                                case "ItemBrand":
                                    var itemBrandName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(itemBrandName, col.ColumnTitle, rowMessage);
                                    if (!itemBrandName.IsNullOrEmpty())
                                    {
                                        if (!itemBrandDic.ContainsKey(itemBrandName)) InvalidException(col.ColumnTitle, rowMessage);
                                        itemBrandId = itemBrandDic[itemBrandName];
                                    }
                                    break;
                                case "ItemGrade":
                                    var itemGradeName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(itemGradeName, col.ColumnTitle, rowMessage);
                                    if (!itemGradeName.IsNullOrEmpty())
                                    {
                                        if (!itemGradeDic.ContainsKey(itemGradeName)) InvalidException(col.ColumnTitle, rowMessage);
                                        itemGradeId = itemGradeDic[itemGradeName];
                                    }
                                    break;
                                case "ItemModel":
                                    var itemModelName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(itemModelName, col.ColumnTitle, rowMessage);
                                    if (!itemModelName.IsNullOrEmpty())
                                    {
                                        if (!itemModelDic.ContainsKey(itemModelName)) InvalidException(col.ColumnTitle, rowMessage);
                                        itemModelId = itemModelDic[itemModelName];
                                    }
                                    break;
                                case "ItemSize":
                                    var itemSizeName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(itemSizeName, col.ColumnTitle, rowMessage);
                                    if (!itemSizeName.IsNullOrEmpty())
                                    {
                                        if (!itemSizeDic.ContainsKey(itemSizeName)) InvalidException(col.ColumnTitle, rowMessage);
                                        itemSizeId = itemSizeDic[itemSizeName];
                                    }
                                    break;
                                case "ItemSeries":
                                    var itemSeriesName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(itemSeriesName, col.ColumnTitle, rowMessage);
                                    if (!itemSeriesName.IsNullOrEmpty())
                                    {
                                        if (!itemSeriesDic.ContainsKey(itemSeriesName)) InvalidException(col.ColumnTitle, rowMessage);
                                        itemSeriesId = itemSeriesDic[itemSeriesName];
                                    }
                                    break;
                                case "ColorPattern":
                                    var colorPatternName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(colorPatternName, col.ColumnTitle, rowMessage);
                                    if (!colorPatternName.IsNullOrEmpty())
                                    {
                                        if (!colorPatternDic.ContainsKey(colorPatternName)) InvalidException(col.ColumnTitle, rowMessage);
                                        colorPatternId = colorPatternDic[colorPatternName];
                                    }
                                    break;
                                case "CPU":
                                    var cpuName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(cpuName, col.ColumnTitle, rowMessage);
                                    if (!cpuName.IsNullOrEmpty())
                                    {
                                        if (!cpuDic.ContainsKey(cpuName)) InvalidException(col.ColumnTitle, rowMessage);
                                        cpuId = cpuDic[cpuName];
                                    }
                                    break;
                                case "RAM":
                                    var ramName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(ramName, col.ColumnTitle, rowMessage);
                                    if (!ramName.IsNullOrEmpty())
                                    {
                                        if (!ramDic.ContainsKey(ramName)) InvalidException(col.ColumnTitle, rowMessage);
                                        ramId = ramDic[ramName];
                                    }
                                    break;
                                case "VGA":
                                    var vgaName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(vgaName, col.ColumnTitle, rowMessage);
                                    if (!vgaName.IsNullOrEmpty())
                                    {
                                        if (!vgaDic.ContainsKey(vgaName)) InvalidException(col.ColumnTitle, rowMessage);
                                        vgaId = vgaDic[vgaName];
                                    }
                                    break;
                                case "HDD":
                                    var hddName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(hddName, col.ColumnTitle, rowMessage);
                                    if (!hddName.IsNullOrEmpty())
                                    {
                                        if (!hddDic.ContainsKey(hddName)) InvalidException(col.ColumnTitle, rowMessage);
                                        hddId = hddDic[hddName];
                                    }
                                    break;
                                case "Screen":
                                    var screenName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(screenName, col.ColumnTitle, rowMessage);
                                    if (!screenName.IsNullOrEmpty())
                                    {
                                        if (!screenDic.ContainsKey(screenName)) InvalidException(col.ColumnTitle, rowMessage);
                                        screenId = screenDic[screenName];
                                    }
                                    break;
                                case "Camera":
                                    var cameraName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(cameraName, col.ColumnTitle, rowMessage);
                                    if (!cameraName.IsNullOrEmpty())
                                    {
                                        if (!cameraDic.ContainsKey(cameraName)) InvalidException(col.ColumnTitle, rowMessage);
                                        cameraId = cameraDic[cameraName];
                                    }
                                    break;
                                case "Battery":
                                    var batteryName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(batteryName, col.ColumnTitle, rowMessage);
                                    if (!batteryName.IsNullOrEmpty())
                                    {
                                        if (!batteryDic.ContainsKey(batteryName)) InvalidException(col.ColumnTitle, rowMessage);
                                        batteryId = batteryDic[batteryName];
                                    }
                                    break;
                                case "FieldA":
                                    var fieldAName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(fieldAName, col.ColumnTitle, rowMessage);
                                    if (!fieldAName.IsNullOrEmpty())
                                    {
                                        if (!fieldADic.ContainsKey(fieldAName)) InvalidException(col.ColumnTitle, rowMessage);
                                        fieldAId = fieldADic[fieldAName];
                                    }
                                    break;
                                case "FieldB":
                                    var fieldBName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(fieldBName, col.ColumnTitle, rowMessage);
                                    if (!fieldBName.IsNullOrEmpty())
                                    {
                                        if (!fieldBDic.ContainsKey(fieldBName)) InvalidException(col.ColumnTitle, rowMessage);
                                        fieldBId = fieldBDic[fieldBName];
                                    }
                                    break;
                                case "FieldC":
                                    var fieldCName = worksheet.GetString(i, col.Index);
                                    if (col.IsRequired) ValidateInput(fieldCName, col.ColumnTitle, rowMessage);
                                    if (!fieldCName.IsNullOrEmpty())
                                    {
                                        if (!fieldCDic.ContainsKey(fieldCName)) InvalidException(col.ColumnTitle, rowMessage);
                                        fieldCId = fieldCDic[fieldCName];
                                    }
                                    break;
                                case "IsModifier":
                                    isModifier = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !isModifier.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "IsAddOn":
                                    isAddOn = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !isAddOn.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "UseBOM":
                                    useBOM = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !useBOM.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                                case "DisplayBOM":
                                    displayBOM = worksheet.GetBoolOrNull(i, col.Index);
                                    if (col.IsRequired && !displayBOM.HasValue) InputException(col.ColumnTitle, rowMessage);
                                    break;
                            }
                        }

                        if (!AccountingFeatureEnable)
                        {
                            if (itemType == ItemType.Inventory)
                            {
                                ValidateSelect(itemSetting.COGSAccountId, L("COGSAccount"), rowMessage);
                                ValidateSelect(itemSetting.InventoryAccountId, L("InventoryAccount"), rowMessage);

                                purchaseAccountId = itemSetting.COGSAccountId;
                                inventoryAccountId = itemSetting.InventoryAccountId;
                            }
                            else if (itemType == ItemType.Asset)
                            {
                                ValidateSelect(itemSetting.COGSAccountId, L("COGSAccount"), rowMessage);
                                ValidateSelect(itemSetting.AssetAccountId, L("AssetAccount"), rowMessage);
                                purchaseAccountId = itemSetting.COGSAccountId;
                                inventoryAccountId = itemSetting.AssetAccountId;
                            }
                            else
                            {
                                ValidateSelect(itemSetting.ExpenseAccountId, L("ExpenseAccount"), rowMessage);
                                purchaseAccountId = itemSetting.ExpenseAccountId;
                            }

                            ValidateSelect(itemSetting.RevenueAccountId, L("RevenueAccount"), rowMessage);
                            saleAccountId = itemSetting.RevenueAccountId;
                        }

                        var entity = Item.Create(
                            input.TenantId.Value,
                            input.UserId.Value,
                            itemType,
                            itemCategory,
                            code,
                            barcode,
                            name,
                            displayName,
                            description,
                            reorderStock,
                            minStock,
                            maxStock,
                            netWeight,
                            grossWeight,
                            width,
                            height,
                            length,
                            diameter,
                            area,
                            volume,
                            weightUnit ?? WeightUnit.kg,
                            lengthUnit ?? LengthUnit.m,
                            areaUnit ?? AreaUnit.m2,
                            volumeUnit ?? VolumeUnit.m3,
                            trackSerial ?? false,
                            trackExpired ?? false,
                            trackBatchNo ?? false,
                            trackAssetStatus ?? false,
                            itemGroupId,
                            itemBrandId,
                            itemGradeId,
                            itemSizeId,
                            colorPatternId,
                            unitId,
                            itemSeriesId,
                            itemModelId,
                            cpuId,
                            ramId,
                            vgaId,
                            screenId,
                            batteryId,
                            cameraId,
                            hddId,
                            fieldAId,
                            fieldBId,
                            fieldCId,
                            inventoryAccountId,
                            purchaseAccountId,
                            saleAccountId,
                            isModifier ?? false,
                            isAddOn ?? false,
                            useBOM ?? false,
                            displayBOM ?? false,
                            altCode
                        );

                        addItems.Add(entity);
                        itemDic.Add(entity.Code, entity.ItemType);
                        if (!entity.Barcode.IsNullOrEmpty()) barcodeHash.Add(entity.Barcode);
                        if (!entity.ALTCode.IsNullOrEmpty()) altCodeHash.Add(entity.ALTCode);
                    }
                }
            }

            if (!addItems.Any()) return IdentityResult.Success;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    await _repository.BulkInsertAsync(addItems);
                }

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }

        public async Task<ExportFileOutput> ExportExcelUpdateItemZonesAsync()
        {
            var items = await _repository.GetAll().AsNoTracking().Where(s => s.IsActive).Select(s => new { s.Code, s.Name }).ToListAsync();

            var excelInput = new ExportDataFileInput
            {
                FileName = "CityProvince.xlsx",
                Items = items,
                Columns = new List<ColumnOutput>
                {
                    new ColumnOutput{ ColumnName = "Code", ColumnTitle = L("Code"), Width = 100, Index = 5, IsRequired = true },
                    new ColumnOutput{ ColumnName = "Name",  ColumnTitle = L("Name_",L("Item")), Width = 250, Index = 1 },
                    new ColumnOutput{ ColumnName = "Zone", ColumnTitle = L("Zone"), Width = 150, Index = 6 }
                }
            };

            return await _excelManager.ExportExcelAsync(excelInput);
        }

        /// <summary>
        /// Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportExcelUpdateItemZonesAsync(IImportExcelEntity<Guid> input)
        {   
            var itemDic = new Dictionary<string, Guid>();
            var zoneDic = new Dictionary<string, KeyValuePair<Guid, Guid>>();
            var itemZones = new List<ItemZone>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    itemDic = await _repository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                    zoneDic = await _zoneRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => new KeyValuePair<Guid, Guid>(v.Id, v.WarehouseId));
                    itemZones = await _itemZoneRepository.GetAll().Include(s => s.Zone).AsNoTracking().ToListAsync();
                }
            }

            var itemWarehouseHash = new HashSet<string>();
            var addItemZoneItems = new List<ItemZone>();
            var updateItemZones = new List<ItemZone>();
            var deleteItemZones = new List<ItemZone>();

            var excelPackage = await _fileStorageManager.DownloadExcel(input.Token);
            if (excelPackage != null)
            {
                // Get the work book in the file
                var workBook = excelPackage.Workbook;
                if (workBook != null)
                {
                    // retrive first worksheets
                    var worksheet = excelPackage.Workbook.Worksheets[0];
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var rowMessage = $", Row: {i}";

                        var code = worksheet.GetString(i, 1);
                        ValidateCodeInput(code, rowMessage);
                        if (!itemDic.ContainsKey(code)) InvalidException(L("ItemCode"), rowMessage);

                        var zoneName = worksheet.GetString(i, 3);
                        ValidateInput(zoneName, L("Zone"), rowMessage);
                        if (!zoneDic.ContainsKey(zoneName)) InvalidException(L("Zone"), rowMessage);

                        var zoneKey = $"{code}-{zoneDic[zoneName].Value}";
                        if(itemWarehouseHash.Contains(zoneKey)) DuplicateException(L("Warehouse"), rowMessage);

                        var findItemZone = itemZones.FirstOrDefault(s => s.ItemId == itemDic[code] && s.Zone.WarehouseId == zoneDic[zoneName].Value);
                        if (findItemZone == null)
                        {   
                            var entity = ItemZone.Create(input.TenantId.Value, input.UserId.Value, itemDic[code], zoneDic[zoneName].Key);
                            addItemZoneItems.Add(entity);
                        }
                        else
                        {
                            findItemZone.Update(input.UserId.Value, itemDic[code], zoneDic[zoneName].Key);
                            updateItemZones.Add(findItemZone);
                        }

                        itemWarehouseHash.Add(zoneKey);
                    }
                }
            }

            if(itemZones.Any()) deleteItemZones = itemZones.Where(s => !updateItemZones.Any(r => r.Id == s.Id)).ToList();

            if (!addItemZoneItems.Any() && !updateItemZones.Any() && !deleteItemZones.Any()) return IdentityResult.Success;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    if(addItemZoneItems.Any()) await _itemZoneRepository.BulkInsertAsync(addItemZoneItems);
                    if (updateItemZones.Any()) await _itemZoneRepository.BulkUpdateAsync(updateItemZones);
                    if (deleteItemZones.Any()) await _itemZoneRepository.BulkDeleteAsync(deleteItemZones);
                }

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }


    }
}
