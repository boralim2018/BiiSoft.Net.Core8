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
using BiiSoft.FileStorages;
using BiiSoft.Warehouses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

        public ItemManager(
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
                input.ItemType == ItemType.SparePart ||
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
                input.ItemType == ItemType.SparePart ||
                input.ItemType == ItemType.Asset)
            {
                var find = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.InventoryAccountId);
                if (!find) InvalidException(L("InventoryAccount"));
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
                input.TrackInventoryStatus,
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
                input.SaleAccountId);

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
                input.TrackInventoryStatus,
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
                input.SaleAccountId);

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

            var formula = await _itemCodeFormulaRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(s => s.ItemTypes.Contains(input.ItemType));

            if(formula == null || formula.Type == ItemCodeFormulaType.Manual) return;

            var prefix = "";
            if(formula.Type == ItemCodeFormulaType.Custom)
            {
                prefix = formula.Prefix;
            }

            var latestCode = await _repository.GetAll()
                            .AsNoTracking()
                            .Where(s => formula.ItemTypes.Contains(s.ItemType))
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

        protected override async Task BeforeInstanceUpdate(Item input, Item entity)
        {
            var itemZones = await _itemZoneRepository.GetAll().AsNoTracking().Where(s => s.ItemId == input.Id).ToListAsync();

            var addZones = new List<ItemZone>();
            var updateZones = new List<ItemZone>();

            foreach (var zone in input.ItemZones)
            {
                var updateZone = itemZones.FirstOrDefault(s => s.ZoneId == zone.ZoneId);
                if(updateZone != null)
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

            if (!input.ItemZones.IsNullOrEmpty()) {
                var addZones = input.ItemZones.Select(s => ItemZone.Create(input.TenantId, input.CreatorUserId.Value, input.Id, s.ZoneId)).ToList();
                await CurrentUnitOfWork.SaveChangesAsync();
                await _itemZoneRepository.BulkInsertAsync(addZones);
            }

            return result;
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"Item.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Item")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ItemType"), Width = 150, IsRequired = true, ColumnType = ColumnType.Lookup, LookupList = ItemType.Service.ToListStr() },
                    new ColumnOutput{ ColumnTitle = L("ItemCategory"), Width = 150, IsRequired = true, ColumnType = ColumnType.Lookup, LookupList = ItemCategory.Service.ToListStr() },
                    new ColumnOutput{ ColumnTitle = L("Unit"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ItemGroup"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("ItemBrand"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("ItemModel"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("ItemGrade"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("ItemSize"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("ItemSeries"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("ColorPattern"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CPU"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("RAM"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("VGA"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("HDD"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Screen"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Camera"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Battery"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("FieldA"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("FieldB"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("FieldC"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("PurchaseAccount"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("SaleAccount"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("InventoryAccount"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("NetWeight"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("GrossWeight"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Width"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Height"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Length"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Diameter"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Area"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Volume"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("WeightUnit"), Width = 100, ColumnType = ColumnType.Lookup, LookupList = WeightUnit.g.ToListStr() },
                    new ColumnOutput{ ColumnTitle = L("LengthUnit"), Width = 100, ColumnType = ColumnType.Lookup, LookupList = LengthUnit.m.ToListStr() },
                    new ColumnOutput{ ColumnTitle = L("AreaUnit"), Width = 100, ColumnType = ColumnType.Lookup, LookupList = AreaUnit.m2.ToListStr() },
                    new ColumnOutput{ ColumnTitle = L("VolumeUnit"), Width = 100, ColumnType = ColumnType.Lookup, LookupList = VolumeUnit.m3.ToListStr() },
                    new ColumnOutput{ ColumnTitle = L("TrackSerial"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("TrackExpired"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("TrackBatchNo"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("TrackInventoryStatus"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("ReorderStock"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("MaxStock"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("MinStock"), Width = 100 },
                    new ColumnOutput{ ColumnTitle = L("Description"), Width = 150 },
                }
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

            var itemDic = new Dictionary<string, KeyValuePair<Guid, ItemType>>();
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
            var accountDic = new Dictionary<string, Guid>();


            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {  
                    itemCodeFormulas = await _itemCodeFormulaRepository.GetAll().AsNoTracking().ToListAsync();
                    itemSetting = await GetItemSettingAsync();

                    if (itemSetting == null) InputException(L("ItemSetting"));

                    itemDic = await _repository.GetAll().AsNoTracking().OrderByDescending(s => s.Code).ToDictionaryAsync(k => k.Code, v => new KeyValuePair<Guid, ItemType>(v.Id, v.ItemType));
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
                    accountDic = await _chartOfAccountRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Name, v => v.Id);
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
                        var rowMessage = $", Row: {i}";

                        var typeTypeName = worksheet.GetString(i, 4);
                        ValidateSelect(typeTypeName, L("ItemType"), rowMessage);
                        var itemType = Enum.Parse<ItemType>(typeTypeName);

                        var code = worksheet.GetString(i, 1);
                        if (!itemSetting.UseCodeFormula)
                        {
                            ValidateCodeInput(code, rowMessage);
                        }
                        else
                        {
                            var formula = itemCodeFormulas.Where(s => s.ItemTypes.Contains(itemType)).FirstOrDefault();

                            if (formula == null) InputException(L("ItemCodeFormula"), rowMessage);
                            
                            if (formula.Type == ItemCodeFormulaType.Manual)
                            {
                                ValidateCodeInput(code, rowMessage);
                            }
                            else if(code.IsNullOrEmpty())
                            {
                                var prefix = formula.Prefix;

                                var latestCode = itemDic
                                                .Where(s => formula.ItemTypes.Contains(s.Value.Value))
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

                        if (itemDic.ContainsKey(code)) DuplicateCodeException(code, rowMessage);

                        var name = worksheet.GetString(i, 2);
                        ValidateName(name, rowMessage);

                        var displayName = worksheet.GetString(i, 3);
                        ValidateDisplayName(displayName, rowMessage);

                        var categoryName = worksheet.GetString(i, 5);
                        ValidateSelect(categoryName, L("ItemCategory"), rowMessage);
                        var itemCategory = Enum.Parse<ItemCategory>(categoryName);

                        var unitName = worksheet.GetString(i, 6);
                        ValidateInput(unitName, L("Unit"), rowMessage);
                        if(unitDic.ContainsKey(unitName)) InvalidException(L("Unit"), rowMessage);
                        Guid? unitId = unitDic[unitName];

                        Guid? itemGroupId = null;
                        if (itemSetting.UseItemGroup)
                        {
                            var itemGroupName = worksheet.GetString(i, 7);
                            if(itemSetting.ItemGroupRequired) ValidateInput(itemGroupName, L("ItemGroup"), rowMessage);
                            if (!itemGroupName.IsNullOrEmpty())
                            {
                                if (!itemGroupDic.ContainsKey(itemGroupName)) InvalidException(L("ItemGroup"), rowMessage);
                                itemGroupId = itemGroupDic[itemGroupName];
                            }
                        }

                        Guid? itemBrandId = null;
                        if (itemSetting.UseBrand)
                        {
                            var itemBrandName = worksheet.GetString(i, 8);
                            if(itemSetting.BrandRequired) ValidateInput(itemBrandName, L("ItemBrand"), rowMessage);
                            if (!itemBrandName.IsNullOrEmpty())
                            {
                                if (!itemBrandDic.ContainsKey(itemBrandName)) InvalidException(L("ItemBrand"), rowMessage);
                                itemBrandId = itemBrandDic[itemBrandName];
                            }
                        }

                        Guid? itemModelId = null;
                        if (itemSetting.UseModel)
                        {
                            var itemModelName = worksheet.GetString(i, 9);
                            if(itemSetting.ModelRequired) ValidateInput(itemModelName, L("ItemModel"), rowMessage);
                            if (!itemModelName.IsNullOrEmpty())
                            {
                                if (!itemModelDic.ContainsKey(itemModelName)) InvalidException(L("ItemModel"), rowMessage);
                                itemModelId = itemModelDic[itemModelName];
                            }
                        }

                        Guid? itemGradeId = null;
                        if (itemSetting.UseGrade)
                        {
                            var itemGradeName = worksheet.GetString(i, 10);
                            if(itemSetting.GradeRequired) ValidateInput(itemGradeName, L("ItemGrade"), rowMessage);
                            if (!itemGradeName.IsNullOrEmpty())
                            {
                                if (!itemGradeDic.ContainsKey(itemGradeName)) InvalidException(L("ItemGrade"), rowMessage);
                                itemGradeId = itemGradeDic[itemGradeName];
                            }
                        }

                        Guid? itemSizeId = null;
                        if (itemSetting.UseSize)
                        {
                            var itemSizeName = worksheet.GetString(i, 11);
                            if(itemSetting.SizeRequired) ValidateInput(itemSizeName, L("ItemSize"), rowMessage);
                            if (!itemSizeName.IsNullOrEmpty())
                            {
                                if (!itemSizeDic.ContainsKey(itemSizeName)) InvalidException(L("ItemSize"), rowMessage);
                                itemSizeId = itemSizeDic[itemSizeName];
                            }
                        }

                        Guid? itemSeriesId = null;
                        if (itemSetting.UseSeries)
                        {
                            var itemSeriesName = worksheet.GetString(i, 12);
                            if(itemSetting.SerialRequired) ValidateInput(itemSeriesName, L("ItemSeries"), rowMessage);
                            if (!itemSeriesName.IsNullOrEmpty())
                            {
                                if (!itemSeriesDic.ContainsKey(itemSeriesName)) InvalidException(L("ItemSeries"), rowMessage);
                                itemSeriesId = itemSeriesDic[itemSeriesName];
                            }
                        }

                        Guid? colorPatternId = null;
                        if (itemSetting.UseColorPattern)
                        {
                            var colorPatternName = worksheet.GetString(i, 13);
                            if(itemSetting.ColorPatternRequired) ValidateInput(colorPatternName, L("ColorPattern"), rowMessage);
                            if (!colorPatternName.IsNullOrEmpty())
                            {
                                if (!colorPatternDic.ContainsKey(colorPatternName)) InvalidException(L("ColorPattern"), rowMessage);
                                colorPatternId = colorPatternDic[colorPatternName];
                            }
                        }

                        Guid? cpuId = null;
                        if (itemSetting.UseCPU)
                        {
                            var cpuName = worksheet.GetString(i, 14);
                            if (itemSetting.CPURequired) ValidateInput(cpuName, L("CPU"), rowMessage);
                            if (!cpuName.IsNullOrEmpty())
                            {
                                if (!cpuDic.ContainsKey(cpuName)) InvalidException(L("CPU"), rowMessage);
                                cpuId = cpuDic[cpuName];
                            }
                        }

                        Guid? ramId = null;
                        if (itemSetting.UseRAM)
                        {
                            var ramName = worksheet.GetString(i, 15);
                            if (itemSetting.RAMRequired) ValidateInput(ramName, L("RAM"), rowMessage);
                            if (!ramName.IsNullOrEmpty())
                            {
                                if (!ramDic.ContainsKey(ramName)) InvalidException(L("RAM"), rowMessage);
                                ramId = ramDic[ramName];
                            }
                        }

                        Guid? vgaId = null;
                        if (itemSetting.UseVGA)
                        {
                            var vgaName = worksheet.GetString(i, 16);
                            if (itemSetting.VGARequired) ValidateInput(vgaName, L("VGA"), rowMessage);
                            if (!vgaName.IsNullOrEmpty())
                            {
                                if (!vgaDic.ContainsKey(vgaName)) InvalidException(L("VGA"), rowMessage);
                                vgaId = vgaDic[vgaName];
                            }
                        }

                        Guid? hddId = null;
                        if (itemSetting.UseHDD)
                        {
                            var hddName = worksheet.GetString(i, 17);
                            if (itemSetting.HDDRequired) ValidateInput(hddName, L("HDD"), rowMessage);
                            if (!hddName.IsNullOrEmpty())
                            {
                                if (!hddDic.ContainsKey(hddName)) InvalidException(L("HDD"), rowMessage);
                                hddId = hddDic[hddName];
                            }
                        }

                        Guid? screenId = null;
                        if (itemSetting.UseScreen)
                        {
                            var screenName = worksheet.GetString(i, 18);
                            if (itemSetting.ScreenRequired) ValidateInput(screenName, L("Screen"), rowMessage);
                            if (!screenName.IsNullOrEmpty())
                            {
                                if (!screenDic.ContainsKey(screenName)) InvalidException(L("Screen"), rowMessage);
                                screenId = screenDic[screenName];
                            }
                        }


                        Guid? cameraId = null;
                        if (itemSetting.UseCamera)
                        {
                            var cameraName = worksheet.GetString(i, 19);
                            if (itemSetting.CameraRequired) ValidateInput(cameraName, L("Camera"), rowMessage);
                            if (!cameraName.IsNullOrEmpty())
                            {
                                if (!cameraDic.ContainsKey(cameraName)) InvalidException(L("Camera"), rowMessage);
                                cameraId = cameraDic[cameraName];
                            }
                        }

                        Guid? batteryId = null;
                        if (itemSetting.UseBattery)
                        {
                            var batteryName = worksheet.GetString(i, 20);
                            if (itemSetting.BatteryRequired) ValidateInput(batteryName, L("Battery"), rowMessage);
                            if (!batteryName.IsNullOrEmpty())
                            {
                                if (!batteryDic.ContainsKey(batteryName)) InvalidException(L("Battery"), rowMessage);
                                batteryId = batteryDic[batteryName];
                            }
                        }

                        Guid? fieldAId = null;
                        if (itemSetting.UseFieldA)
                        {
                            var fieldAName = worksheet.GetString(i, 21);
                            if (itemSetting.FieldARequired) ValidateInput(fieldAName, L("FieldA"), rowMessage);
                            if (!fieldAName.IsNullOrEmpty())
                            {
                                if (!fieldADic.ContainsKey(fieldAName)) InvalidException(L("FieldA"), rowMessage);
                                fieldAId = fieldADic[fieldAName];
                            }
                        }

                        Guid? fieldBId = null;
                        if (itemSetting.UseFieldB)
                        {
                            var fieldBName = worksheet.GetString(i, 22);
                            if (itemSetting.FieldBRequired) ValidateInput(fieldBName, L("FieldB"), rowMessage);
                            if (!fieldBName.IsNullOrEmpty())
                            {
                                if (!fieldBDic.ContainsKey(fieldBName)) InvalidException(L("FieldB"), rowMessage);
                                fieldBId = fieldBDic[fieldBName];
                            }
                        }

                        Guid? fieldCId = null;
                        if (itemSetting.UseFieldC)
                        {
                            var fieldCName = worksheet.GetString(i, 23);
                            if (itemSetting.FieldCRequired) ValidateInput(fieldCName, L("FieldC"), rowMessage);
                            if (!fieldCName.IsNullOrEmpty())
                            {
                                if (!fieldCDic.ContainsKey(fieldCName)) InvalidException(L("FieldC"), rowMessage);
                                fieldCId = fieldCDic[fieldCName];
                            }
                        }

                        var purchaseAccountName = worksheet.GetString(i, 24);
                        ValidateInput(purchaseAccountName, L("PurchaseAccount"), rowMessage);
                        if (!accountDic.ContainsKey(purchaseAccountName)) InvalidException(L("PurchaseAccount"), rowMessage);
                        Guid? purchaseAccountId = accountDic[purchaseAccountName];

                        var saleAccountName = worksheet.GetString(i, 25);
                        ValidateInput(saleAccountName, L("SaleAccount"), rowMessage);
                        if (!accountDic.ContainsKey(saleAccountName)) InvalidException(L("SaleAccount"), rowMessage);
                        Guid? saleAccountId = accountDic[saleAccountName];

                        var inventoryAccountName = worksheet.GetString(i, 26);
                        ValidateInput(inventoryAccountName, L("InventoryAccount"), rowMessage);
                        if (!accountDic.ContainsKey(inventoryAccountName)) InvalidException(L("InventoryAccount"), rowMessage);
                        Guid? inventoryAccountId = accountDic[inventoryAccountName];

                        decimal netWeight = worksheet.GetDecimal(i, 27);
                        if (itemSetting.NetWeightRequired && netWeight == 0) InputException(L("NetWeight"), rowMessage);

                        decimal grossWeight = worksheet.GetDecimal(i, 28);
                        if (itemSetting.GrossWeightRequired && grossWeight == 0) InputException(L("GrossWeight"), rowMessage);

                        decimal width = worksheet.GetDecimal(i, 29);
                        if (itemSetting.WidthRequired && width == 0) InputException(L("Width"), rowMessage);

                        decimal height = worksheet.GetDecimal(i, 30);
                        if (itemSetting.HeightRequired && height == 0) InputException(L("Height"), rowMessage);

                        decimal length = worksheet.GetDecimal(i, 31);
                        if (itemSetting.LengthRequired && length == 0) InputException(L("Length"), rowMessage);

                        decimal diameter = worksheet.GetDecimal(i, 32);
                        if (itemSetting.DiameterRequired && diameter == 0) InputException(L("Diameter"), rowMessage);

                        decimal area = worksheet.GetDecimal(i, 33);
                        if (itemSetting.AreaRequired && area == 0) InputException(L("Area"), rowMessage);

                        decimal volume = worksheet.GetDecimal(i, 34);
                        if (itemSetting.VolumeRequired && volume == 0) InputException(L("Volume"), rowMessage);

                        WeightUnit? weightUnit = null;
                        var weightUnitName = worksheet.GetString(i, 35);
                        if(itemSetting.NetWeightRequired || itemSetting.GrossWeightRequired) ValidateSelect(weightUnitName, L("WeightUnit"), rowMessage);
                        if(!weightUnitName.IsNullOrEmpty()) weightUnit = Enum.Parse<WeightUnit>(weightUnitName);

                        LengthUnit? lengthUnit = null;
                        var lengthUnitName = worksheet.GetString(i, 36);
                        if (itemSetting.WidthRequired || itemSetting.HeightRequired || itemSetting.LengthRequired || itemSetting.DiameterRequired) ValidateSelect(lengthUnitName, L("LengthUnit"), rowMessage);
                        if (!lengthUnitName.IsNullOrEmpty()) lengthUnit = Enum.Parse<LengthUnit>(lengthUnitName);

                        AreaUnit? areaUnit = null;
                        var areaUnitName = worksheet.GetString(i, 37);
                        if (itemSetting.AreaRequired) ValidateSelect(areaUnitName, L("AreaUnit"), rowMessage);
                        if (!areaUnitName.IsNullOrEmpty()) areaUnit = Enum.Parse<AreaUnit>(areaUnitName);

                        VolumeUnit? volumeUnit = null;
                        var volumeUnitName = worksheet.GetString(i, 38);
                        if (itemSetting.VolumeRequired) ValidateSelect(volumeUnitName, L("VolumeUnit"), rowMessage);
                        if (!volumeUnitName.IsNullOrEmpty()) volumeUnit = Enum.Parse<VolumeUnit>(volumeUnitName);

                        bool? trackSerial = worksheet.GetBoolOrNull(i, 39);                       
                        if (itemSetting.SerialRequired && trackSerial == null) InputException(L("TrackSerial"), rowMessage);

                        bool? trackExpired = worksheet.GetBoolOrNull(i, 40);
                        if (itemSetting.ExpiredRequired && trackExpired == null) InputException(L("TrackExpired"), rowMessage);

                        bool? trackBatchNo = worksheet.GetBoolOrNull(i, 41);
                        if (itemSetting.BatchNoRequired && trackBatchNo == null) InputException(L("TrackBatchNo"), rowMessage);

                        bool? trackInventoryStatus = worksheet.GetBoolOrNull(i, 42);
                        if (itemSetting.BatchNoRequired && trackInventoryStatus == null) InputException(L("TrackInventoryStatus"), rowMessage);

                        decimal reorderStock = worksheet.GetDecimal(i, 43);
                        if (itemSetting.ReorderStockRequired && reorderStock == 0) InputException(L("ReorderStock"), rowMessage);

                        decimal maxStock = worksheet.GetDecimal(i, 44);
                        if (itemSetting.MaxStockRequired && maxStock == 0) InputException(L("MaxStock"), rowMessage);

                        decimal minStock = worksheet.GetDecimal(i, 45);
                        if (itemSetting.MinStockRequired && minStock == 0) InputException(L("MinStock"), rowMessage);

                        var description = worksheet.GetString(i, 46);

                        var entity = Item.Create(
                            input.TenantId.Value, 
                            input.UserId.Value, 
                            itemType, 
                            itemCategory, 
                            code, 
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
                            weightUnit??WeightUnit.kg,
                            lengthUnit??LengthUnit.m,
                            areaUnit??AreaUnit.m2,
                            volumeUnit??VolumeUnit.m3,
                            trackSerial??false,
                            trackExpired??false,
                            trackBatchNo??false,
                            trackInventoryStatus??false,
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
                            saleAccountId);

                        addItems.Add(entity);
                        itemDic.Add(entity.Code, new KeyValuePair<Guid, ItemType>(entity.Id, entity.ItemType));
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

    }
}
