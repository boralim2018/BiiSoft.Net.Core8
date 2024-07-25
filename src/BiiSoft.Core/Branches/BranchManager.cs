using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using BiiSoft.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BiiSoft.Authorization.Users;
using BiiSoft.Locations;
using Abp.Extensions;

namespace BiiSoft.Branches
{
    public class BranchManager : BiiSoftDefaultNameActiveValidateServiceBase<Branch, Guid>, IBranchManager
    {

        protected readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        protected readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        protected readonly IBiiSoftRepository<KhanDistrict, Guid> _khanDistrictRepository;
        protected readonly IBiiSoftRepository<SangkatCommune, Guid> _sangkatCommuneRepository;
        protected readonly IBiiSoftRepository<Village, Guid> _villageRepository;
        protected readonly IBiiSoftRepository<Location, Guid> _locationRepository;

        private readonly IFileStorageManager _fileStorageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBranchContactAddressManager _branchContactAddressManager;
        private readonly IBiiSoftRepository<BranchContactAddress, Guid> _branchContactAddressRepository;
        private readonly IBiiSoftRepository<UserBranch, Guid> _userBranchRepository;
        public BranchManager(
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<Village, Guid> villageRepository,
            IBiiSoftRepository<Location, Guid> locationRepository,
            IBiiSoftRepository<UserBranch, Guid> userBranchRepository,
            IBiiSoftRepository<BranchContactAddress, Guid> branchContactAddressRepository,
            IBranchContactAddressManager branchContactAddressManager,
            IBiiSoftRepository<Branch, Guid> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager) : base(repository)
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _branchContactAddressManager = branchContactAddressManager;
            _branchContactAddressRepository = branchContactAddressRepository;
            _userBranchRepository = userBranchRepository;
            _countryRepository = countryRepository;
            _cityProvinceRepository = cityProvinceRepository;
            _khanDistrictRepository = khanDistrictRepository;
            _sangkatCommuneRepository = sangkatCommuneRepository;
            _villageRepository = villageRepository;
        }

        #region override base class
        protected override string InstanceName => L("Branch");
        protected override bool IsUniqueName => true;

        protected override Branch CreateInstance(int? tenantId, long userId, Branch input)
        {
            return Branch.Create(tenantId.Value, userId, input.Name, input.DisplayName, input.BusinessId, input.PhoneNumber, input.Email, input.Website, input.TaxRegistrationNumber);
        }

        protected override void UpdateInstance(long userId, Branch input, Branch entity)
        {
            entity.Update(userId, input.Name, input.DisplayName, input.BusinessId, input.PhoneNumber, input.Email, input.Website, input.TaxRegistrationNumber);

            input.TenantId = entity.TenantId; //Rquired in UpdateAsync Method
        }

        #endregion

        private void ValidateAddress(List<BranchContactAddress> addresses)
        {
            if (addresses.IsNullOrEmpty()) InputException(L("ContactAddress"));
        }

        public async Task<IdentityResult> InsertAsync(int? tenantId, long userId, Branch input, List<BranchContactAddress> addresses)
        {   
            ValidateAddress(addresses);

            await ValidateInputAsync(input);

            var entity = CreateInstance(tenantId, userId, input);

            await _repository.BulkInsertAsync(new List<Branch> { entity });

            input.Id = entity.Id;

            foreach (var address in addresses)
            {
                address.SetBranch(input.Id);
            }

            await _branchContactAddressManager.BulkInsertAsync(tenantId, userId, addresses);

            return IdentityResult.Success;
        }

        public async Task<Branch> GetCompanyInfoAsync()
        {
            return await _repository.GetAll().AsNoTracking().FirstOrDefaultAsync(u => u.IsDefault);
        }

        public override async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var members = await _userBranchRepository.GetAll().AsNoTracking().AnyAsync(s => s.BranchId == id);
            if (members) ErrorException(L("AlreadyHasMembers", InstanceName));

            var address = await _branchContactAddressRepository.GetAll().AsNoTracking().Where(s => s.BranchId == id).ToListAsync();
            await _branchContactAddressRepository.BulkDeleteAsync(address);

            return await base.DeleteAsync(id);
        }

        public async Task<IdentityResult> UpdateAsync(long userId, Branch input, List<BranchContactAddress> addresses)
        {
            ValidateAddress(addresses);

            await base.UpdateAsync(userId, input);

            foreach (var address in addresses)
            {
                address.SetBranch(input.Id);
            }

            await _branchContactAddressManager.BulkSyncAsync(input.TenantId, userId, input.Id, addresses);

            return IdentityResult.Success;
        }

        /// <summary>
        /// Import data from excel file template. Must call in close connection
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="fileToken"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<IdentityResult> ImportAsync(int? tenantId, long userId, string fileToken)
        {
            var branchs = new List<Branch>();
            var branchHash = new HashSet<string>();
            var addressDic = new Dictionary<string, BranchContactAddress>();
            var defaultBranch = "";

            var countryDic = new Dictionary<string, Guid>();
            var cityProvinceDic = new Dictionary<string, Guid>();
            var khanDistrictDic = new Dictionary<string, Guid>();
            var sangkatCommuneDic = new Dictionary<string, Guid>();
            var villageDic = new Dictionary<string, Guid>();
            var locationDic = new Dictionary<string, Guid>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    countryDic = await _countryRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.ISO, v => v.Id);
                    cityProvinceDic = await _cityProvinceRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.ISO, v => v.Id);
                    khanDistrictDic = await _khanDistrictRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                    sangkatCommuneDic = await _sangkatCommuneRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                    villageDic = await _villageRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                    locationDic = await _locationRepository.GetAll().AsNoTracking().ToDictionaryAsync(k => k.Code, v => v.Id);
                }
            }

            //var excelPackage = Read(input, _appFolders);
            var excelPackage = await _fileStorageManager.DownloadExcel(fileToken);
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

                        var name = worksheet.GetString(i, 1);
                        ValidateName(name, $", Row: {i}");
                        if (branchHash.Contains(name)) throw DuplicateNameException(name, $", Row = {i}");

                        var displayName = worksheet.GetString(i, 2);
                        ValidateDisplayName(name, $", Row: {i}");

                        var businessId = worksheet.GetString(i, 3);
                        var phone = worksheet.GetString(i, 4);
                        var email = worksheet.GetString(i, 5);
                        var website = worksheet.GetString(i, 6);
                        var taxNumber = worksheet.GetString(i, 7);

                        var country = worksheet.GetString(i, 8);
                        ValidateInput(country, L("Country") + $", Row = {i}");
                        if (!countryDic.ContainsKey(country)) InvalidException(L("Country") + $", Row = {i}");
                        var countryId = countryDic[country];

                        var cityProvince = worksheet.GetString(i, 9);
                        Guid? cityProvinceId = null;
                        if (!cityProvince.IsNullOrWhiteSpace())
                        {
                            if (!cityProvinceDic.ContainsKey(cityProvince)) InvalidException(L("CityProvince") + $", Row = {i}");
                            cityProvinceId = cityProvinceDic[cityProvince];
                        }

                        var khanDistrict = worksheet.GetString(i, 10);
                        Guid? khanDistrictId = null;
                        if (!khanDistrict.IsNullOrWhiteSpace())
                        {
                            if (!khanDistrictDic.ContainsKey(khanDistrict)) InvalidException(L("KhanDistrict") + $", Row = {i}");
                            khanDistrictId = khanDistrictDic[khanDistrict];
                        }
                        
                        var sangkatCommune = worksheet.GetString(i, 10);
                        Guid? sangkatCommuneId = null;
                        if (!sangkatCommune.IsNullOrWhiteSpace())
                        {
                            if (!sangkatCommuneDic.ContainsKey(sangkatCommune)) InvalidException(L("SangkatCommune") + $", Row = {i}");
                            sangkatCommuneId = sangkatCommuneDic[sangkatCommune];
                        }

                        var village = worksheet.GetString(i, 10);
                        Guid? villageId = null;
                        if (!village.IsNullOrWhiteSpace())
                        {
                            if (!villageDic.ContainsKey(village)) InvalidException(L("Village") + $", Row = {i}");
                            villageId = villageDic[village];
                        }

                        var location = worksheet.GetString(i, 11);
                        Guid? locationId = null;
                        if (!location.IsNullOrWhiteSpace())
                        {
                            if (!locationDic.ContainsKey(location)) InvalidException(L("Location") + $", Row = {i}");
                            locationId = locationDic[location];
                        }

                        var postalCode = worksheet.GetString(i, 11);
                        var street = worksheet.GetString(i, 12);
                        var houseNo = worksheet.GetString(i, 13);

                        var isDefault = worksheet.GetBool(i, 14);
                        if (isDefault && defaultBranch != "") MoreThanException(L("Default"), 1.ToString(), $", Row = {i}");
                        else if (isDefault) defaultBranch = name;

                        var cannotEdit = worksheet.GetBool(i, 15);
                        var cannotDelete = worksheet.GetBool(i, 16);

                        var entity = Branch.Create(tenantId.Value, userId, name, displayName, businessId, phone, email, website, taxNumber);
                        if(isDefault) entity.SetDefault(isDefault);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        var address = BranchContactAddress.Create(tenantId.Value, userId, entity.Id, countryId, cityProvinceId, khanDistrictId, sangkatCommuneId, villageId, locationId, postalCode, street, houseNo);
                        address.SetDefault(true);
                        addressDic.Add(entity.Name, address);

                        branchs.Add(entity);
                        branchHash.Add(entity.Name);
                    }
                }
            }

            if (!branchs.Any()) return IdentityResult.Success;

            var updateBranchDic = new Dictionary<string, Branch>();
            var updateAddressDic = new Dictionary<string, BranchContactAddress>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    updateBranchDic = await _repository.GetAll().AsNoTracking()
                               .Where(s => branchHash.Contains(s.Name))
                               .ToDictionaryAsync(k => k.Name, v => v);

                    if (updateBranchDic.Any())
                    {
                        updateAddressDic = await _branchContactAddressRepository.GetAll().AsNoTracking()
                                                 .Where(s => s.IsDefault)
                                                 .Where(s => updateBranchDic.ContainsKey(s.Branch.Name))
                                                 .ToDictionaryAsync(k => k.Branch.Name, v => v);
                    }
                }
            }

            var addBranchs = new List<Branch>();
            var addAddresses = new List<BranchContactAddress>();

            foreach (var l in branchs)
            {
                if (updateBranchDic.ContainsKey(l.Name))
                {
                    updateBranchDic[l.Name].Update(userId, l.Name, l.DisplayName, l.BusinessId, l.PhoneNumber, l.Email, l.Website, l.TaxRegistrationNumber);
                    updateBranchDic[l.Name].SetCannotEdit(l.CannotEdit);
                    updateBranchDic[l.Name].SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addBranchs.Add(l);
                }

                if (updateAddressDic.ContainsKey(l.Name))
                {
                    var a = addressDic[l.Name];
                    updateAddressDic[l.Name].Update(userId, updateBranchDic[l.Name].Id, a.CountryId, a.CityProvinceId, a.KhanDistrictId, a.SangkatCommuneId, a.VillageId, a.LocationId, a.PostalCode, a.Street, a.HouseNo);
                }
                else
                {
                    addAddresses.Add(addressDic[l.Name]);
                }

            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    if (updateBranchDic.Any()) await _repository.BulkUpdateAsync(updateBranchDic.Values.ToList());
                    if (addBranchs.Any()) await _repository.BulkInsertAsync(addBranchs);
                    if (updateAddressDic.Any()) await _branchContactAddressRepository.BulkUpdateAsync(updateAddressDic.Values.ToList());
                    if (addAddresses.Any()) await _branchContactAddressRepository.BulkInsertAsync(addAddresses);
                }

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
