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
using BiiSoft.ContactInfo;

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
        private readonly IContactAddressManager _contactAddressManager;
        private readonly IBiiSoftRepository<ContactAddress, Guid> _contactAddressRepository;
        private readonly IBiiSoftRepository<UserBranch, Guid> _userBranchRepository;
        public BranchManager(
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<Village, Guid> villageRepository,
            IBiiSoftRepository<Location, Guid> locationRepository,
            IBiiSoftRepository<UserBranch, Guid> userBranchRepository,
            IBiiSoftRepository<ContactAddress, Guid> contactAddressRepository,
            IContactAddressManager contactAddressManager,
            IBiiSoftRepository<Branch, Guid> repository,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager) : base(repository)
        {
            _fileStorageManager = fileStorageManager;
            _unitOfWorkManager = unitOfWorkManager;
            _contactAddressManager = contactAddressManager;
            _contactAddressRepository = contactAddressRepository;
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
            return Branch.Create(tenantId.Value, userId, input.Name, input.DisplayName, input.BusinessId, input.PhoneNumber, input.Email, input.Website, input.TaxRegistrationNumber, input.BillingAddressId, input.SameAsBillingAddress, input.ShippingAddressId);
        }

        protected override void UpdateInstance(long userId, Branch input, Branch entity)
        {
            entity.Update(userId, input.Name, input.DisplayName, input.BusinessId, input.PhoneNumber, input.Email, input.Website, input.TaxRegistrationNumber, input.BillingAddressId, input.SameAsBillingAddress, input.ShippingAddressId);

            input.TenantId = entity.TenantId; //Rquired in UpdateAsync Method
        }

        #endregion

        public override async Task<IdentityResult> InsertAsync(int? tenantId, long userId, Branch input)
        {
            await ValidateInputAsync(input);

            await _contactAddressManager.InsertAsync(tenantId, userId, input.BillingAddress);
            input.BillingAddressId = input.BillingAddress.Id;

            if (!input.SameAsBillingAddress)
            {
                await _contactAddressManager.InsertAsync(tenantId, userId, input.ShippingAddress);
                input.ShippingAddressId = input.ShippingAddress.Id;
            }

            var entity = CreateInstance(tenantId, userId, input);

            await _repository.InsertAsync(entity);
            input.Id = entity.Id;
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> UpdateAsync(long userId, Branch input)
        {
            await ValidateInputAsync(input);

            var entity = await GetAsync(input.Id, false);
            if (entity == null) NotFoundException(InstanceName);
            ValidateEditable(entity);

            await _contactAddressManager.UpdateAsync(userId, input.BillingAddress);
            input.BillingAddressId = input.BillingAddress.Id;

            Guid? deleteShippingAddressId = null;
            if (input.SameAsBillingAddress)
            {
                if (!entity.SameAsBillingAddress) deleteShippingAddressId = entity.ShippingAddressId;
            }
            else
            {
                if (entity.SameAsBillingAddress)
                {
                    await _contactAddressManager.InsertAsync(entity.TenantId, userId, input.ShippingAddress);
                }
                else
                {
                    await _contactAddressManager.UpdateAsync(userId, input.ShippingAddress);
                }
                input.ShippingAddressId = input.ShippingAddress.Id;
            }

            UpdateInstance(userId, input, entity);

            await _repository.UpdateAsync(@entity);

            if(deleteShippingAddressId.HasValue) await _contactAddressManager.DeleteAsync(deleteShippingAddressId.Value);

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity == null) NotFoundException(InstanceName);
            ValidateDeletable(entity);

            var members = await _userBranchRepository.GetAll().AsNoTracking().AnyAsync(s => s.BranchId == id);
            if (members) ErrorException(L("AlreadyHasMembers", InstanceName));

            var address = await _contactAddressRepository.GetAll().AsNoTracking()
                                .Where(s => s.Id == entity.BillingAddressId || (!entity.SameAsBillingAddress && s.Id == entity.ShippingAddressId))
                                .ToListAsync();
           
            await _repository.DeleteAsync(entity);
            if(address.Any()) await _contactAddressRepository.BulkDeleteAsync(address);

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
                        if (!countryDic.ContainsKey(country)) InvalidException(L("Country"), $", Row = {i}");
                        var countryId = countryDic[country];

                        var cityProvince = worksheet.GetString(i, 9);
                        Guid? cityProvinceId = null;
                        if (!cityProvince.IsNullOrWhiteSpace())
                        {
                            if (!cityProvinceDic.ContainsKey(cityProvince)) InvalidException(L("CityProvince"), $", Row = {i}");
                            cityProvinceId = cityProvinceDic[cityProvince];
                        }

                        var khanDistrict = worksheet.GetString(i, 10);
                        Guid? khanDistrictId = null;
                        if (!khanDistrict.IsNullOrWhiteSpace())
                        {
                            if (!khanDistrictDic.ContainsKey(khanDistrict)) InvalidException(L("KhanDistrict"), $", Row = {i}");
                            khanDistrictId = khanDistrictDic[khanDistrict];
                        }
                        
                        var sangkatCommune = worksheet.GetString(i, 11);
                        Guid? sangkatCommuneId = null;
                        if (!sangkatCommune.IsNullOrWhiteSpace())
                        {
                            if (!sangkatCommuneDic.ContainsKey(sangkatCommune)) InvalidException(L("SangkatCommune"), $", Row = {i}");
                            sangkatCommuneId = sangkatCommuneDic[sangkatCommune];
                        }

                        var village = worksheet.GetString(i, 12);
                        Guid? villageId = null;
                        if (!village.IsNullOrWhiteSpace())
                        {
                            if (!villageDic.ContainsKey(village)) InvalidException(L("Village"), $", Row = {i}");
                            villageId = villageDic[village];
                        }

                        var postalCode = worksheet.GetString(i, 13);
                        var street = worksheet.GetString(i, 14);
                        var houseNo = worksheet.GetString(i, 15);

                        var sameAsBillingAddress = worksheet.GetBool(i, 16);

                        ContactAddress shippingAddress = null;
                        if (!sameAsBillingAddress) 
                        {
                            var country2 = worksheet.GetString(i, 17);
                            ValidateInput(country2, L("Country") + $", Row = {i}");
                            if (!countryDic.ContainsKey(country2)) InvalidException(L("Country"), $", Row = {i}");
                            var countryId2 = countryDic[country2];

                            var cityProvince2 = worksheet.GetString(i, 18);
                            Guid? cityProvinceId2 = null;
                            if (!cityProvince.IsNullOrWhiteSpace())
                            {
                                if (!cityProvinceDic.ContainsKey(cityProvince2)) InvalidException(L("CityProvince"), $", Row = {i}");
                                cityProvinceId2 = cityProvinceDic[cityProvince2];
                            }

                            var khanDistrict2 = worksheet.GetString(i, 19);
                            Guid? khanDistrictId2 = null;
                            if (!khanDistrict2.IsNullOrWhiteSpace())
                            {
                                if (!khanDistrictDic.ContainsKey(khanDistrict2)) InvalidException(L("KhanDistrict"), $", Row = {i}");
                                khanDistrictId2 = khanDistrictDic[khanDistrict2];
                            }

                            var sangkatCommune2 = worksheet.GetString(i, 20);
                            Guid? sangkatCommuneId2 = null;
                            if (!sangkatCommune2.IsNullOrWhiteSpace())
                            {
                                if (!sangkatCommuneDic.ContainsKey(sangkatCommune2)) InvalidException(L("SangkatCommune"), $", Row = {i}");
                                sangkatCommuneId2 = sangkatCommuneDic[sangkatCommune2];
                            }

                            var village2 = worksheet.GetString(i, 21);
                            Guid? villageId2 = null;
                            if (!village2.IsNullOrWhiteSpace())
                            {
                                if (!villageDic.ContainsKey(village2)) InvalidException(L("Village"), $", Row = {i}");
                                villageId = villageDic[village2];
                            }

                            var postalCode2 = worksheet.GetString(i, 22);
                            var street2 = worksheet.GetString(i, 23);
                            var houseNo2 = worksheet.GetString(i, 24);

                            shippingAddress = ContactAddress.Create(tenantId.Value, userId, countryId2, cityProvinceId2, khanDistrictId2, sangkatCommuneId2, villageId2, null, postalCode2, street2, houseNo2);
                        }

                        var isDefault = worksheet.GetBool(i, 25);
                        if (isDefault && defaultBranch != "") MoreThanException(L("Default"), 1.ToString(), $", Row = {i}");
                        else if (isDefault) defaultBranch = name;

                        var cannotEdit = worksheet.GetBool(i, 26);
                        var cannotDelete = worksheet.GetBool(i, 27);

                        var billingAddress = ContactAddress.Create(tenantId.Value, userId, countryId, cityProvinceId, khanDistrictId, sangkatCommuneId, villageId, null, postalCode, street, houseNo);

                        var entity = Branch.Create(tenantId.Value, userId, name, displayName, businessId, phone, email, website, taxNumber, billingAddress.Id, sameAsBillingAddress, sameAsBillingAddress ? billingAddress.Id : shippingAddress.Id);
                        if(isDefault) entity.SetDefault(isDefault);
                        entity.SetCannotEdit(cannotEdit);
                        entity.SetCannotDelete(cannotDelete);

                        entity.BillingAddress = billingAddress;
                        entity.ShippingAddress = shippingAddress;
                       
                        branchs.Add(entity);
                        branchHash.Add(entity.Name);
                    }
                }
            }

            if (!branchs.Any()) return IdentityResult.Success;

            var updateBranchDic = new Dictionary<string, Branch>();
            var updateAddressDic = new Dictionary<Guid, ContactAddress>();

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    updateBranchDic = await _repository.GetAll().AsNoTracking()
                               .Where(s => branchHash.Contains(s.Name))
                               .ToDictionaryAsync(k => k.Name, v => v);

                    if (updateBranchDic.Any())
                    {
                        var updateAddressIds = updateBranchDic.Values.SelectMany(s =>
                        {
                            List<Guid> ids = new List<Guid> { s.BillingAddressId };
                            if (!s.SameAsBillingAddress) ids.Add(s.ShippingAddressId);
                            return ids;
                        })
                        .GroupBy(s => s)
                        .Select(s => s.Key)
                        .ToHashSet();

                        updateAddressDic = await _contactAddressRepository.GetAll().AsNoTracking()
                                                 .Where(s => updateAddressIds.Contains(s.Id))
                                                 .ToDictionaryAsync(k => k.Id, v => v);
                    }
                }
            }

            var addBranchs = new List<Branch>();
            var addAddresses = new List<ContactAddress>();
            var updateAddresses = new List<ContactAddress>();
            var deleteAddresses = new List<ContactAddress>();

            foreach (var l in branchs)
            {
                if (updateBranchDic.ContainsKey(l.Name))
                {
                    var updateBranch = updateBranchDic[l.Name];

                    var updateAddress = updateAddressDic.ContainsKey(updateBranch.BillingAddressId) ? updateAddressDic[updateBranch.BillingAddressId] : null;
                    if (updateAddress != null)
                    {
                        updateAddress.Update(userId, updateBranch.BillingAddress.CountryId, updateBranch.BillingAddress.CityProvinceId, updateBranch.BillingAddress.KhanDistrictId, updateBranch.BillingAddress.SangkatCommuneId, updateBranch.BillingAddress.VillageId, updateBranch.BillingAddress.LocationId, updateBranch.BillingAddress.PostalCode, updateBranch.BillingAddress.Street, updateBranch.BillingAddress.HouseNo);
                        updateBranch.BillingAddressId = updateAddress.Id;
                        updateAddresses.Add(updateAddress);
                    }
                    else
                    {
                        addAddresses.Add(updateBranch.BillingAddress);
                    }

                    if (l.SameAsBillingAddress)
                    {
                        if (!updateBranch.SameAsBillingAddress)
                        {
                            var deleteAddress = updateAddressDic.ContainsKey(updateBranch.ShippingAddressId) ? updateAddressDic[updateBranch.ShippingAddressId] : null;
                            if(deleteAddress != null) deleteAddresses.Add(deleteAddress);
                        }
                    }
                    else
                    {
                        if (updateBranch.SameAsBillingAddress)
                        {
                            addAddresses.Add(updateBranch.ShippingAddress);
                        }
                        else
                        {
                            var updateAddress2 = updateAddressDic.ContainsKey(updateBranch.ShippingAddressId) ? updateAddressDic[updateBranch.ShippingAddressId] : null;
                            if (updateAddress2 != null)
                            {
                                updateAddress2.Update(userId, updateBranch.ShippingAddress.CountryId, updateBranch.ShippingAddress.CityProvinceId, updateBranch.ShippingAddress.KhanDistrictId, updateBranch.ShippingAddress.SangkatCommuneId, updateBranch.ShippingAddress.VillageId, updateBranch.ShippingAddress.LocationId, updateBranch.ShippingAddress.PostalCode, updateBranch.ShippingAddress.Street, updateBranch.ShippingAddress.HouseNo);
                                updateBranch.ShippingAddressId = updateAddress2.Id;
                                updateAddresses.Add(updateAddress2);
                            }
                            else
                            {
                                addAddresses.Add(updateBranch.ShippingAddress);
                            }
                        }
                    }

                    updateBranch.Update(userId, l.Name, l.DisplayName, l.BusinessId, l.PhoneNumber, l.Email, l.Website, l.TaxRegistrationNumber, l.BillingAddressId, l.SameAsBillingAddress, l.ShippingAddressId);
                    updateBranch.SetCannotEdit(l.CannotEdit);
                    updateBranch.SetCannotDelete(l.CannotDelete);
                }
                else
                {
                    addBranchs.Add(l);
                }
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    if (updateAddresses.Any()) await _contactAddressRepository.BulkUpdateAsync(updateAddresses);
                    if (addAddresses.Any()) await _contactAddressRepository.BulkInsertAsync(addAddresses);

                    if (updateBranchDic.Any()) await _repository.BulkUpdateAsync(updateBranchDic.Values.ToList());
                    if (addBranchs.Any()) await _repository.BulkInsertAsync(addBranchs);
                   
                    if(deleteAddresses.Any()) await _contactAddressRepository.BulkDeleteAsync(deleteAddresses);
                }

                await uow.CompleteAsync();
            }

            return IdentityResult.Success;
        }
    }
}
