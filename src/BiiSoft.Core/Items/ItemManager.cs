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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                input.ItemType == ItemType.FixedAsset)
            {
                ValidateSelect(input.InventoryAccountId, L("InventoryAccount"));
            }
        }

        protected override async Task ValidateInputAsync(Item input)
        {
            await base.ValidateInputAsync(input);

            var findCode = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Code == input.Code && s.Id != input.Id);
            if (findCode) DuplicateCodeException(input.Code);

            if (!input.ItemGroupId.IsNullOrEmpty())
            {
                var find = await _itemGroupRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemGroupId);
                if (!find) InvalidException(L("ItemGroup"));
            }
            if (!input.ItemBrandId.IsNullOrEmpty())
            {
                var find = await _itemBrandRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemBrandId);
                if (!find) InvalidException(L("ItemBrand"));
            }
            if (!input.ItemGradeId.IsNullOrEmpty())
            {
                var find = await _itemGradeRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemGradeId);
                if (!find) InvalidException(L("ItemGrade"));
            }
            if (!input.ItemModelId.IsNullOrEmpty())
            {
                var find = await _itemModelRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemModelId);
                if (!find) InvalidException(L("ItemModel"));
            }
            if (!input.ItemSizeId.IsNullOrEmpty())
            {
                var find = await _itemSizeRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemSizeId);
                if (!find) InvalidException(L("ItemSize"));
            }
            if (!input.ItemSeriesId.IsNullOrEmpty())
            {
                var find = await _itemSeriesRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ItemSeriesId);
                if (!find) InvalidException(L("ItemSeries"));
            }
            if (!input.ColorPatternId.IsNullOrEmpty())
            {
                var find = await _colorPatternRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ColorPatternId);
                if (!find) InvalidException(L("ColorPattern"));
            }
            if (!input.CPUId.IsNullOrEmpty())
            {
                var find = await _cpuRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CPUId);
                if (!find) InvalidException(L("CPU"));
            }
            if (!input.RAMId.IsNullOrEmpty())
            {
                var find = await _ramRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.RAMId);
                if (!find) InvalidException(L("RAM"));
            }
            if (!input.VGAId.IsNullOrEmpty())
            {
                var find = await _vgaRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.VGAId);
                if (!find) InvalidException(L("VGA"));
            }
            if (!input.HDDId.IsNullOrEmpty())
            {
                var find = await _hddRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.HDDId);
                if (!find) InvalidException(L("HDD"));
            }
            if (!input.ScreenId.IsNullOrEmpty())
            {
                var find = await _screenRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.ScreenId);
                if (!find) InvalidException(L("Screen"));
            }
            if (!input.CameraId.IsNullOrEmpty())
            {
                var find = await _cameraRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CameraId);
                if (!find) InvalidException(L("Camera"));
            }
            if (!input.BatteryId.IsNullOrEmpty())
            {
                var find = await _batteryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.BatteryId);
                if (!find) InvalidException(L("Battery"));
            }
            if (!input.FieldAId.IsNullOrEmpty())
            {
                var find = await _fieldARepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.FieldAId);
                if (!find) InvalidException(L("FieldA"));
            }
            if (!input.FieldBId.IsNullOrEmpty())
            {
                var find = await _fieldBRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.FieldBId);
                if (!find) InvalidException(L("FieldB"));
            }
            if (!input.FieldCId.IsNullOrEmpty())
            {
                var find = await _fieldCRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.FieldCId);
                if (!find) InvalidException(L("FieldC"));
            }
            if (!input.UnitId.IsNullOrEmpty())
            {
                var find = await _unitRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.UnitId);
                if (!find) InvalidException(L("Unit"));
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
            if (!input.InventoryAccountId.IsNullOrEmpty())
            {
                var find = await _chartOfAccountRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.InventoryAccountId);
                if (!find) InvalidException(L("InventoryAccount"));
            }
        }

        protected override Item CreateInstance(Item input)
        {
            return Item.Create(
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
                input.SaleAccountId,
                input.PurchaseTaxId,
                input.SaleTaxId);
        }

        private async Task<bool> CheckAutoGenerateCodeAsync()
        {
            return false;
        }

        protected override async Task BeforeInstanceUpdate(Item input, Item entity)
        {
            var autoGenerateCode = await CheckAutoGenerateCodeAsync();

            //if (autoGenerateCode && entity.SubAccountType != input.SubAccountType) await SetCodeAsync(input);
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
                input.SaleAccountId,
                input.PurchaseTaxId,
                input.SaleTaxId);
        }

        #endregion

        private async Task<string> GetLatestCodeAsync()
        {
            var prefix = "ITM-";

            return await _repository.GetAll()
                            .AsNoTracking()
                            .Where(s => s.Code.StartsWith(prefix))
                            .Select(s => s.Code)
                            .OrderByDescending(s => s)
                            .FirstOrDefaultAsync();
        }


        private async Task SetCodeAsync(Item input)
        {
            if (!input.Code.IsNullOrWhiteSpace()) return;

            var prefix = "ITM-";
            var latestCode = await GetLatestCodeAsync();

            if (latestCode.IsNullOrWhiteSpace())
            {
                input.SetCode(0.GenerateCode(8, prefix));
            }
            else
            {
                input.SetCode(latestCode.NextCode(prefix));
            }
        }

        public override async Task<IdentityResult> InsertAsync(Item input)
        {
            var autoGenerateCode = await CheckAutoGenerateCodeAsync();
            if (autoGenerateCode) await SetCodeAsync(input);
            return await base.InsertAsync(input);
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var fileInput = new ExportFileInput
            {
                FileName = $"Item.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Item")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("SubAccountType"), Width = 150, IsRequired = true},
                    new ColumnOutput{ ColumnTitle = L("ParentAccount"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
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
            var accounts = new List<Item>();
            var autoGenerateCode = false;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    accounts = await _repository.GetAll().AsNoTracking().ToListAsync();
                    autoGenerateCode = await CheckAutoGenerateCodeAsync();
                }
            }

            //var addAccounts = new List<Item>();
            //var latestCodeDic = accounts.GroupBy(s => s.SubAccountType)
            //                            .ToDictionary(k => k.Key, v => v.MaxBy(m => m.Code).Code);

            //
            //var excelPackage = await _fileStorageManager.DownloadExcel(input.Token);
            //if (excelPackage != null)
            //{
            //    // Get the work book in the file
            //    var workBook = excelPackage.Workbook;
            //    if (workBook != null)
            //    {
            //        // retrive first worksheets
            //        var worksheet = excelPackage.Workbook.Worksheets[0];
            //        for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            //        {
            //            var code = worksheet.GetString(i, 1);
            //            if (!autoGenerateCode) ValidateCodeInput(code, $", Row: {i}");

            //            var name = worksheet.GetString(i, 2);
            //            ValidateName(name, $", Row: {i}");

            //            var findName = accounts.Any(a => a.Name == name);
            //            if(findName) DuplicateNameException(name, $", Row: {i}");

            //            var displayName = worksheet.GetString(i, 3);
            //            ValidateDisplayName(displayName, $", Row: {i}");

            //            var findDisplayName = accounts.Any(a => a.DisplayName == displayName);
            //            if (findDisplayName) DuplicateNameException(displayName, $", Row: {i}");

            //            var subtAccount = worksheet.GetString(i, 4)?.Replace(" ", "");
            //            ValidateInput(subtAccount, L("SubAccountType"), $", Row: {i}");
            //            SubAccountType subAccountType = (SubAccountType)Enum.Parse(typeof(SubAccountType), subtAccount);

            //            Guid? parentId = null;
            //            var parent = worksheet.GetString(i, 5);
            //            if (!parent.IsNullOrWhiteSpace())
            //            {
            //                var find = accounts.FirstOrDefault(a => a.Name.ToLower() == parent.ToLower().Trim());
            //                if (find == null) InvalidException(L("ParentAccount"), $", Row: {i}");
            //                if (find.ParentId != null) ErrorException(L("SubAccountCannotUseAsParent") + $", Row: {i}");

            //                parentId = find.Id;
            //            }

            //            var cannotEdit = worksheet.GetBool(i, 6);
            //            var cannotDelete = worksheet.GetBool(i, 7);

            //            if (autoGenerateCode)
            //            {
            //                if (code.IsNullOrWhiteSpace())
            //                {
            //                    if (latestCodeDic.ContainsKey(subAccountType))
            //                    {
            //                        code = latestCodeDic[subAccountType].NextCode(subAccountType.ToIntStr());
            //                    }
            //                    else
            //                    {
            //                        code = 0.GenerateCode(BiiSoftConsts.ItemCodeLength, subAccountType.ToIntStr());
            //                    }
            //                }
            //                else 
            //                {
            //                    if (!code.IsCode(BiiSoftConsts.ItemCodeLength, subAccountType.ToIntStr())) InvalidCodeException(code, $", Row: {i}");
            //                }

            //                if (!latestCodeDic.ContainsKey(subAccountType))
            //                {
            //                    latestCodeDic.Add(subAccountType, code);
            //                }
            //                else if (code.CompareTo(latestCodeDic[subAccountType]) > 0)
            //                {
            //                     latestCodeDic[subAccountType] = code;
            //                }
            //            }

            //            var findCode = accounts.Any(a => a.Code == code);
            //            if (findCode) DuplicateCodeException(code, $", Row: {i}");

            //            var entity = Item.Create(input.TenantId.Value, input.UserId.Value, subAccountType, code, name, displayName, parentId);


            //            addAccounts.Add(entity);
            //            accounts.Add(entity);
            //        }
            //    }
            //}

            //if (!addAccounts.Any()) return IdentityResult.Success;

            //using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            //{
            //    using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
            //    {
            //       await _repository.BulkInsertAsync(addAccounts);
            //    }

            //    await uow.CompleteAsync();
            //}

            return IdentityResult.Success;
        }

    }
}
