using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.Branches.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Extensions;
using BiiSoft.Authorization.Users;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Extensions;
using BiiSoft.FileStorages;
using BiiSoft.Folders;
using Abp.Domain.Uow;
using System.Transactions;

namespace BiiSoft.Branches
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class BranchAppService : BiiSoftAppServiceBase, IBranchAppService
    {
        private readonly IBranchManager _branchManager;
        private readonly IBiiSoftRepository<Branch, Guid> _branchRepository;
        private readonly IBiiSoftRepository<BranchContactAddress, Guid> _branchContactAddressRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BranchAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            IBranchManager branchManager,
            IBiiSoftRepository<Branch, Guid> branchRepository,
            IBiiSoftRepository<BranchContactAddress, Guid> branchContactAddressRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _branchManager=branchManager;
            _branchRepository=branchRepository;
            _branchContactAddressRepository=branchContactAddressRepository;
            _userRepository=userRepository;
            _fileStorageManager=fileStorageManager;
            _appFolders=appFolders;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Create)]
        public async Task<Guid> Create(CreateUpdateBranchInputDto input)
        {
            var entity = ObjectMapper.Map<Branch>(input);
            var contactAddresses = ObjectMapper.Map<List<BranchContactAddress>>(input.ContactAddresses);
            
            await _branchManager.InsertAsync(AbpSession.TenantId, AbpSession.UserId.Value, entity, contactAddresses);
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _branchManager.DeleteAsync(input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            await _branchManager.DisableAsync(AbpSession.UserId.Value, input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            await _branchManager.EnableAsync(AbpSession.UserId.Value, input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            await _branchManager.SetAsDefaultAsync(AbpSession.UserId.Value, input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Branches)]
        public async Task<PagedResultDto<FindBranchDto>> Find(PageBranchInputDto input)
        {
            var query = from l in _branchRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                                    (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                                    (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                                .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                                    (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                                    (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                                    s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        select new FindBranchDto
                        {
                            Id = l.Id,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            BusinessId = l.BusinessId,
                            PhoneNumber = l.PhoneNumber,  
                            IsActive = l.IsActive,
                            IsDefault = l.IsDefault                            
                        };

            var totalCount = await query.CountAsync();
            var items = new List<FindBranchDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<FindBranchDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_View, PermissionNames.Pages_Company_Branches_Edit)]
        public async Task<BranchDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = from l in _branchRepository.GetAll()
                                .AsNoTracking()
                                .Where(s => s.Id == input.Id)
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new BranchDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            BusinessId = l.BusinessId,
                            PhoneNumber = l.PhoneNumber,
                            Email = l.Email,
                            Website = l.Website,
                            TaxRegistrationNumber = l.TaxRegistrationNumber,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = m == null ? "" : m.UserName
                        };

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            var record = await _branchRepository.GetAll()
                               .AsNoTracking()
                               .OrderBy(s => s.Id)
                               .GroupBy(s => 1)
                                .Select(s => new
                                {
                                    First = s.Where(r => r.No < result.No).Select(n => new { n.No, n.Id }).OrderBy(o => o.No).FirstOrDefault(),
                                    Pervious = s.Where(r => r.No < result.No).Select(n => new { n.No, n.Id }).OrderByDescending(o => o.No).FirstOrDefault(),
                                    Next = s.Where(r => r.No > result.No).Select(n => new { n.No, n.Id }).OrderBy(o => o.No).FirstOrDefault(),
                                    Last = s.Where(r => r.No > result.No).Select(n => new { n.No, n.Id }).OrderByDescending(o => o.No).FirstOrDefault(),
                                })
                               .FirstOrDefaultAsync();

            if (record.First != null) result.FirstId = record.First.Id;
            if (record.Pervious != null) result.PreviousId = record.Pervious.Id;
            if (record.Next != null) result.NextId = record.Next.Id;
            if (record.Last != null) result.LastId = record.Last.Id;

            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var addresses = await _branchContactAddressRepository.GetAll().AsNoTracking().Where(s => s.BranchId == input.Id).Select(s => 
            new BranchContactAddressDto
            {
                Id = s.Id,                
                CountryId = s.CountryId,
                CityProvinceId = s.CityProvinceId,
                KhanDistrictId = s.KhanDistrictId,
                SangkatCommuneId = s.SangkatCommuneId,
                VillageId = s.VillageId,
                LocationId = s.LocationId,
                CountryName = !s.CountryId.HasValue ? "" : isDefaultLanguage ? s.Country.Name : s.Country.DisplayName,
                CityProvinceName = !s.CityProvinceId.HasValue ? "" : isDefaultLanguage ? s.CityProvince.Name : s.CityProvince.DisplayName,
                KhanDistrictName = !s.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? s.KhanDistrict.Name : s.KhanDistrict.DisplayName,
                SangkatCommuneName = !s.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? s.SangkatCommune.Name : s.SangkatCommune.DisplayName,
                VillageName = !s.VillageId.HasValue ? "" : isDefaultLanguage ? s.Village.Name : s.Village.DisplayName,
                LocationName = !s.LocationId.HasValue ? "" : isDefaultLanguage ? s.Location.Name : s.Location.DisplayName,
                PostalCode = s.PostalCode,
                Street = s.Street,
                HouseNo = s.HouseNo,
                IsDefault = s.IsDefault
            })
           .ToListAsync();

            result.ContactAddresses = addresses;

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Company_Branches)]
        public async Task<PagedResultDto<BranchListDto>> GetList(PageBranchInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<BranchListDto>> GetListHelper(PageBranchInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = from l in _branchRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                                    (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                                    (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                                .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                                    (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                                    (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                                    s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        join a in _branchContactAddressRepository.GetAll().AsNoTracking().Where(s => s.IsDefault)
                        on l.Id equals a.BranchId
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new BranchListDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            BusinessId = l.BusinessId,
                            PhoneNumber = l.PhoneNumber,
                            Email = l.Email,
                            Website = l.Website,
                            TaxRegistrationNumber = l.TaxRegistrationNumber,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModifierUserId = u.LastModifierUserId,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserName = m == null ? "" : m.UserName,
                            CountryName = !a.CountryId.HasValue ? "" : isDefaultLanguage ? a.Country.Name : a.Country.DisplayName,
                            CityProvinceName = !a.CityProvinceId.HasValue ? "" : isDefaultLanguage ? a.CityProvince.Name : a.CityProvince.DisplayName,
                            KhanDistrictName = !a.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? a.KhanDistrict.Name : a.KhanDistrict.DisplayName,
                            SangkatCommuneName = !a.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? a.SangkatCommune.Name : a.SangkatCommune.DisplayName,
                            VillageName = !a.VillageId.HasValue ? "" : isDefaultLanguage ? a.Village.Name : a.Village.DisplayName,
                            PostalCode = a.PostalCode,
                            Street = a.Street,
                            HouseNo = a.HouseNo
                        };

            var totalCount = await query.CountAsync();
            var items = new List<BranchListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<BranchListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Company_Branches_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelBranchInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            var result = new ExportFileOutput
            {
                FileName = "Branch.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            PagedResultDto<BranchListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = input.Columns.
                    Where(s => s.Visible)
                    .OrderBy(s => s.Index)
                    .ToList();

                //foreach (var i in displayColumns)
                //{
                //    ws.AddTextToCell(rowTableHeader, colHeaderTable, i.ColumnTitle, true);
                //    if (i.Width > 0) ws.Column(colHeaderTable).Width = i.Width.PixcelToInches();

                //    colHeaderTable += 1;
                //}
                #endregion Row 1

                var rowIndex = rowTableHeader + 1;
                foreach (var row in listResult.Items)
                {
                    var colIndex = 1;
                    foreach (var col in displayColumns)
                    {
                        var value = row.GetType().GetProperty(col.ColumnName.ToPascalCase()).GetValue(row);

                        if (col.ColumnName == "CreatorUserName")
                        {
                            var newValue = value;
                            if(row.CreationTime.HasValue) newValue += $"\r\n{Convert.ToDateTime(row.CreationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                            col.WriteCell(ws, rowIndex, colIndex, newValue);
                        }
                        else if (col.ColumnName == "LastModifierUserName")
                        {
                            var newValue = value;
                            if(row.LastModificationTime.HasValue) newValue += $"\r\n{Convert.ToDateTime(row.LastModificationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                            col.WriteCell(ws, rowIndex, colIndex, newValue);
                        }
                        else
                        {
                            col.WriteCell(ws, rowIndex, colIndex, value);
                        }

                        colIndex++;
                    }
                    rowIndex++;
                }

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, rowIndex - 1);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;

        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            var result = new ExportFileOutput
            {
                FileName = "Branch.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = new List<ColumnOutput> { 
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Branch")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("BusinessId"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("PhoneNumber"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Email"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Website"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("TaxRegistrationNumber"), Width = 150, IsRequired = true },

                    new ColumnOutput{ ColumnTitle = L("Country"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("CityProvince"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("KhanDistrict"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("SangkatCommune"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Village"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Location"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("PostalCode"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Street"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("HouseNo"), Width = 150 },
                    
                    new ColumnOutput{ ColumnTitle = L("Default"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
                };

                #endregion Row 1

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, 5);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            await _branchManager.ImportAsync(AbpSession.TenantId, AbpSession.UserId.Value, input.Token);
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Edit)]
        public async Task Update(CreateUpdateBranchInputDto input)
        {
            var entity = ObjectMapper.Map<Branch>(input);
            var contactAddresses = ObjectMapper.Map<List<BranchContactAddress>>(input.ContactAddresses);

            await _branchManager.UpdateAsync(AbpSession.UserId.Value, entity, contactAddresses);
        }
    }
}
