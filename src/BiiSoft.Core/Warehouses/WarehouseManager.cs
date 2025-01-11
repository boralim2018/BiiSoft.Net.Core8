using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using BiiSoft.BFiles.Dto;
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
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BiiSoft.Warehouses
{
    public class WarehouseManager : BiiSoftDefaultNameActiveValidateServiceBase<Warehouse, Guid>, IWarehouseManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        private readonly IBiiSoftRepository<Zone, Guid> _zoneRepository;
        private readonly IBiiSoftRepository<WarehouseBranch, Guid> _warehouseBranchRepository;

        public WarehouseManager(
            IExcelManager excelManager,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Zone, Guid> zoneRepository,
            IBiiSoftRepository<WarehouseBranch, Guid> warehouseBranchRepository,
            IBiiSoftRepository<Warehouse, Guid> repository) : base(repository) 
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _excelManager = excelManager;
            _zoneRepository = zoneRepository;
            _warehouseBranchRepository = warehouseBranchRepository;
        }

        #region override
        protected string InstanceKeyName => "Warehouse"; 
        protected override string InstanceName => L(InstanceKeyName);
        protected override bool IsUniqueName => true;

        protected override void ValidateInput(Warehouse input)
        {
            ValidateCodeInput(input.Code);
            base.ValidateInput(input);

            if (input.Sharing == BranchSharing.SpecificBranch) 
            {
               if(input.WarehouseBranches.IsNullOrEmpty()) InputException(L("Branches"));

                var duplicateBranch = input.WarehouseBranches.GroupBy(a => a.BranchId).Any(r => r.Count() > 1);
                if (duplicateBranch) DuplicateException(L("Branch"));
            }
        }

        protected override async Task ValidateInputAsync(Warehouse input)
        {  
            await base.ValidateInputAsync(input);

            var findCode = await _repository.GetAll().AsNoTracking().AnyAsync(s => s.Id != input.Id && s.Code == input.Code);
            if (findCode) DuplicateCodeException(input.Code);
        }

        protected override Warehouse CreateInstance(Warehouse input)
        {
            return Warehouse.Create(input.TenantId.Value, input.CreatorUserId.Value, input.Name, input.DisplayName, input.Code, input.Sharing);
        }
        protected override void UpdateInstance(Warehouse input, Warehouse entity)
        {
            entity.Update(input.LastModifierUserId.Value, input.Name, input.DisplayName, input.Code);
            entity.SetSharing(input.Sharing);
        }

        #endregion


        public override async Task<IdentityResult> InsertAsync(Warehouse input)
        {
            var result = await base.InsertAsync(input);

            if(input.Sharing == BranchSharing.SpecificBranch)
            {
                await CurrentUnitOfWork.SaveChangesAsync();
                var addBranchs = input.WarehouseBranches.Select(s => WarehouseBranch.Create(input.TenantId.Value, input.CreatorUserId.Value, input.Id, s.BranchId)).ToList();
                await _warehouseBranchRepository.BulkInsertAsync(addBranchs);
            }
           
            return result;
        }

        public override async Task<IdentityResult> UpdateAsync(Warehouse input)
        {
            var result = await base.UpdateAsync(input);

            var branches = await _warehouseBranchRepository.GetAll().AsNoTracking().Where(s => s.WarehouseId == input.Id).ToListAsync();
            if (input.Sharing == BranchSharing.SpecificBranch)
            {
                var addBranches = new List<WarehouseBranch>();
                var updateBranches = new List<WarehouseBranch>();

                foreach(var branch in input.WarehouseBranches)
                {
                    if(branch.Id == Guid.Empty)
                    {
                        addBranches.Add(WarehouseBranch.Create(input.TenantId.Value, input.CreatorUserId.Value, input.Id, branch.BranchId));
                    }
                    else
                    {
                        var updateBranch = branches.FirstOrDefault(s => s.Id == branch.Id);
                        if (updateBranch == null) NotFoundException("Branch");

                        updateBranch.Update(input.LastModifierUserId.Value, input.Id, branch.BranchId);
                        updateBranches.Add(updateBranch);
                    }
                }

                if (addBranches.Any()) await _warehouseBranchRepository.BulkInsertAsync(addBranches);
                if (updateBranches.Any()) await _warehouseBranchRepository.BulkUpdateAsync(updateBranches);
                
                var deleteBranches = branches.Where(s => !updateBranches.Any(r => r.Id == s.Id)).ToList();
                if (deleteBranches.Any()) await _warehouseBranchRepository.BulkDeleteAsync(deleteBranches);
            }
            else if (branches.Any()) 
            { 
                await _warehouseBranchRepository.BulkDeleteAsync(branches);
            }

            return result;
        }

        public override async Task<IdentityResult> DeleteAsync(Guid input)
        {
            var entity = await FindAsync(input);
            if (entity == null) NotFoundException(InstanceName);

            var inUse = await _zoneRepository.GetAll().AsNoTracking().AnyAsync(s => s.WarehouseId == input);
            if (inUse) ErrorException(L("IsInUse", L("Warehouse")));

            var branches = await _warehouseBranchRepository.GetAll().AsNoTracking().Where(s => s.WarehouseId ==  input).ToListAsync();
            if(branches.Any()) await _warehouseBranchRepository.BulkDeleteAsync(branches);

            await _repository.DeleteAsync(entity);
            return IdentityResult.Success;
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync()
        {
            var inputFile = new ExportFileInput
            {
                FileName = $"{InstanceKeyName}.xlsx",
                Columns = new List<ColumnOutput> {
                    new ColumnOutput{ ColumnTitle = L("Name_",InstanceName), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 250 },
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
            var entities = new List<Warehouse>();
            var entityHash = new HashSet<string>();
            var codeHash = new HashSet<string>();
          
            
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

                        var code = worksheet.GetString(i, 3);
                        ValidateCodeInput(code, rowInfo);
                        if (codeHash.Contains(code)) DuplicateCodeException(code, rowInfo);
                        codeHash.Add(code);
                       

                        var isDefault = worksheet.GetBool(i, 4);

                        var entity = Warehouse.Create(input.TenantId.Value, input.UserId.Value, name, displayName, code, BranchSharing.All);
                        entity.SetDefault(isDefault);

                        entities.Add(entity);
                        entityHash.Add(name);
                    }
                }
            }

            if (!entities.Any()) return IdentityResult.Success;

            var updateColorPatternDic = new Dictionary<string, Warehouse>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
                {
                    updateColorPatternDic = await _repository.GetAll().AsNoTracking()
                                              .Where(s => entityHash.Contains(s.Name))
                                              .ToDictionaryAsync(k => k.Name, v => v);
                }
            }

            var addColorPatterns = new List<Warehouse>();

            foreach (var l in entities)
            {
                if (updateColorPatternDic.ContainsKey(l.Name))
                {
                    updateColorPatternDic[l.Name].Update(input.UserId.Value, l.Name, l.DisplayName, l.Code);
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
    }
}
