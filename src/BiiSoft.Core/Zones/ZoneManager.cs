using Abp.Domain.Uow;
using Abp.UI;
using BiiSoft.BFiles.Dto;
using BiiSoft.Columns;
using BiiSoft.Entities;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using BiiSoft.FileStorages;
using BiiSoft.Warehouses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BiiSoft.Zones
{
    public class ZoneManager : BiiSoftDefaultNameActiveValidateServiceBase<Zone, Guid>, IZoneManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        private readonly IBiiSoftRepository<Warehouse, Guid> _warehouseRepository;

        public ZoneManager(
            IExcelManager excelManager,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Warehouse, Guid> warehouseRepository,
            IBiiSoftRepository<Zone, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _excelManager = excelManager;
            _warehouseRepository = warehouseRepository;
        }

        #region override
        protected string InstanceKeyName => "Zone"; 
        protected override string InstanceName => L(InstanceKeyName);
        protected override bool IsUniqueName => true;

        protected override void ValidateInput(Zone input)
        {
            base.ValidateInput(input);
            ValidateSelect(input.WarehouseId, L("Warehouse"));
        }

        protected override async Task ValidateInputAsync(Zone input)
        {  
            await base.ValidateInputAsync(input);

            var find = await _warehouseRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.WarehouseId);
            if (!find) InvalidException(L("Warehouse"));
        }

        protected override Zone CreateInstance(Zone input)
        {
            return Zone.Create(input.TenantId.Value, input.CreatorUserId.Value, input.WarehouseId, input.Name, input.DisplayName);
        }
        protected override void UpdateInstance(Zone input, Zone entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.WarehouseId, input.Name, input.DisplayName);
        }

        #endregion

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var inputFile = new ExportFileInput
            {
                FileName = $"{InstanceKeyName}.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Name_",InstanceName), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Warehouse"), Width = 250 },
                    new ColumnOutput{ ColumnTitle = L("Default"), Width = 150 },
                }
            };

            return await _excelManager.ExportExcelTemplateAsync(inputFile);
        }

        /// <summary>
        ///  Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportExcelAsync(IImportExcelEntity<Guid> input)
        {
            var entities = new List<Zone>();
            var entityHash = new HashSet<string>();
            var warehouseDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    warehouseDic = await _warehouseRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                }
            }

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
                        var rowInfo = $", Row = {i}";

                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, rowInfo);
                        if (entityHash.Contains(name)) DuplicateNameException(name, rowInfo);

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(displayName, rowInfo);

                        var warehouse = worksheet.GetString(i, 3);
                        ValidateInput(warehouse, rowInfo);
                        if (!warehouseDic.ContainsKey(warehouse)) InvalidException(L("Warehouse"), rowInfo);
                        
                        var isDefault = worksheet.GetBool(i, 4);

                        var entity = Zone.Create(input.TenantId.Value, input.UserId.Value, warehouseDic[warehouse], name, displayName);
                        entity.SetDefault(isDefault);

                        entities.Add(entity);
                        entityHash.Add(name);
                    }
                }
            }

            if (!entities.Any()) return IdentityResult.Success;

            var updateColorPatternDic = new Dictionary<string, Zone>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    updateColorPatternDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => entityHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
                }
            }

            var addColorPatterns = new List<Zone>();

            foreach (var l in entities)
            {
                if (updateColorPatternDic.ContainsKey(l.Name))
                {
                    updateColorPatternDic[l.Name].Update(input.UserId.Value, l.WarehouseId, l.Name, l.DisplayName);
                    updateColorPatternDic[l.Name].SetDefault(l.IsDefault);
                }
                else
                {
                    addColorPatterns.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    if (updateColorPatternDic.Any()) await _repository.BulkUpdateAsync(updateColorPatternDic.Values.ToList());
                    if (addColorPatterns.Any()) await _repository.BulkInsertAsync(addColorPatterns);
                }
                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }

        protected override async Task<List<Zone>> GetOtherDefaultAsync(Zone input)
        {
            return await _repository.GetAll().Where(s => !s.Id.Equals(input.Id) && s.IsDefault && s.WarehouseId == input.WarehouseId).AsNoTracking().ToListAsync();
        }

        public async Task<Zone> GetDefaultValueAsync(Guid WarehouseId)
        {
            return await _repository.GetAll().AsNoTracking().FirstAsync(s => s.IsActive && s.IsDefault && s.WarehouseId == WarehouseId);
        }
    }
}
