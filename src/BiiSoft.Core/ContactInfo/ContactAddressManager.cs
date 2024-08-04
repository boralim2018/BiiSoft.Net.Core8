using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using BiiSoft.Locations;
using Microsoft.EntityFrameworkCore;
using BiiSoft.Extensions;
using Microsoft.AspNetCore.Identity;
using Abp.Timing;
using Abp.Domain.Entities;
using BiiSoft.Entities;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;

namespace BiiSoft.ContactInfo
{
    public class ContactAddressManager : BiiSoftValidateServiceBase<ContactAddress, Guid>, IContactAddressManager
    {

        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        private readonly IBiiSoftRepository<KhanDistrict, Guid> _khanDistrictRepository;
        private readonly IBiiSoftRepository<SangkatCommune, Guid> _sangkatCommuneRepository;
        private readonly IBiiSoftRepository<Village, Guid> _villageRepository;
        private readonly IBiiSoftRepository<Location, Guid> _locationRepository;
        private readonly ILocationManager _locationManager;

        public ContactAddressManager(
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<Village, Guid> villageRepository,
            IBiiSoftRepository<Location, Guid> locationRepository,
            ILocationManager locationManager,
            IBiiSoftRepository<ContactAddress, Guid> repository) : base(repository)
        {
            _countryRepository = countryRepository;
            _cityProvinceRepository = cityProvinceRepository;
            _khanDistrictRepository = khanDistrictRepository;
            _sangkatCommuneRepository = sangkatCommuneRepository;
            _villageRepository = villageRepository;
            _locationRepository = locationRepository;
            _locationManager = locationManager;
        }

        protected override string InstanceName => L("ContactAddress");

        protected override ContactAddress CreateInstance(ContactAddress input)
        {
            return ContactAddress.Create(input.TenantId, input.CreatorUserId, input.CountryId, input.CityProvinceId, input.KhanDistrictId, input.SangkatCommuneId, input.VillageId, input.LocationId, input.PostalCode, input.Street, input.HouseNo);
        }

        protected override void UpdateInstance(ContactAddress input, ContactAddress entity)
        {
            entity.Update(input.LastModifierUserId, input.CountryId, input.CityProvinceId, input.KhanDistrictId, input.SangkatCommuneId, input.VillageId, input.LocationId, input.PostalCode, input.Street, input.HouseNo);
        }

        protected override async Task ValidateInputAsync(ContactAddress input)
        {
            ValidateSelect(input.CountryId, L("Country"));

            var findCountry = await _countryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CountryId);
            if (!findCountry) InvalidException(L("Country"));

            if (!input.CityProvinceId.IsNullOrEmpty())
            {
                var findCityProvince = await _cityProvinceRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CityProvinceId);
                if (!findCityProvince) InvalidException(L("CityProvince"));
            }

            if (!input.KhanDistrictId.IsNullOrEmpty())
            {
                var findKhanDistrict = await _khanDistrictRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.KhanDistrictId);
                if (!findKhanDistrict) InvalidException(L("KhanDistrict"));
            }

            if (!input.SangkatCommuneId.IsNullOrEmpty())
            {
                var findSangkatCommune = await _sangkatCommuneRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.SangkatCommuneId);
                if (!findSangkatCommune) InvalidException(L("SangkatCommune"));
            }

            if (!input.VillageId.IsNullOrEmpty())
            {
                var findVillage = await _villageRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.VillageId);
                if (!findVillage) InvalidException(L("Village"));
            }

            if (!input.LocationId.IsNullOrEmpty())
            {
                var findLocation = await _locationRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.LocationId);
                if (!findLocation) InvalidException(L("Location"));
            }
        }

        protected virtual void BulkValidate(List<ContactAddress> input)
        {
            var find = input.Any(s => s.CountryId.IsNullOrEmpty());
            if (find) SelectException(L("Country"));
        }

        public async virtual Task BulkValidateAsync(List<ContactAddress> input)
        {
            BulkValidate(input);

            var countries = input.Where(s => !s.CountryId.IsNullOrEmpty()).GroupBy(s => s.CountryId).Select(s => s.Key).ToHashSet();
            var findCountry = await _countryRepository.GetAll().AsNoTracking().CountAsync(s => countries.Contains(s.Id)) == countries.Count;
            if (!findCountry) InvalidException(L("Country"));

            var cityProvinces = input.Where(s => !s.CityProvinceId.IsNullOrEmpty()).GroupBy(s => s.CityProvinceId).Select(s => s.Key).ToHashSet();
            if (cityProvinces.Any())
            {
                var findCityProvince = await _cityProvinceRepository.GetAll().AsNoTracking().CountAsync(s => cityProvinces.Contains(s.Id)) == cityProvinces.Count;
                if (!findCityProvince) InvalidException(L("CityProvince"));
            }

            var khanDistricts = input.Where(s => !s.KhanDistrictId.IsNullOrEmpty()).GroupBy(s => s.KhanDistrictId).Select(s => s.Key).ToHashSet();
            if (khanDistricts.Any())
            {
                var findKhanDistrict = await _khanDistrictRepository.GetAll().AsNoTracking().CountAsync(s => khanDistricts.Contains(s.Id)) == khanDistricts.Count;
                if (!findKhanDistrict) InvalidException(L("KhanDistrict"));
            }

            var sangkatCommunes = input.Where(s => !s.SangkatCommuneId.IsNullOrEmpty()).GroupBy(s => s.SangkatCommuneId).Select(s => s.Key).ToHashSet();
            if (sangkatCommunes.Any())
            {
                var findSangkatCommune = await _sangkatCommuneRepository.GetAll().AsNoTracking().CountAsync(s => sangkatCommunes.Contains(s.Id)) == sangkatCommunes.Count;
                if (!findSangkatCommune) InvalidException(L("SangkatCommune"));
            }

            var villages = input.Where(s => !s.VillageId.IsNullOrEmpty()).GroupBy(s => s.VillageId).Select(s => s.Key).ToHashSet();
            if (villages.Any())
            {
                var findVillage = await _villageRepository.GetAll().AsNoTracking().CountAsync(s => villages.Contains(s.Id)) == villages.Count;
                if (!findVillage) InvalidException(L("Village"));
            }

            var locations = input.Where(s => !s.LocationId.IsNullOrEmpty()).GroupBy(s => s.LocationId).Select(s => s.Key).ToHashSet();
            if (locations.Any())
            {
                var findLocation = await _locationRepository.GetAll().AsNoTracking().CountAsync(s => locations.Contains(s.Id)) == locations.Count;
                if (!findLocation) InvalidException(L("Location"));
            }

        }

        public async Task<IdentityResult> BulkInsertAsync(IMayHaveTenantBulkInputEntity<ContactAddress> input)
        {
            await BulkValidateAsync(input.Items);

            var entities = input.Items.Select(s => {
                s.TenantId = input.TenantId.Value;
                s.CreatorUserId = input.UserId;

                var e = CreateInstance(s);
                s.Id = e.Id;

                return e;
            }).ToList();

            await _repository.BulkInsertAsync(entities);          
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkUpdateAsync(IBulkInputIntity<ContactAddress> input)
        {
            await BulkValidateAsync(input.Items);

            var entities = await _repository.GetAll().AsNoTracking().Where(s => input.Items.Any(r => r.Id.Equals(s.Id))).ToDictionaryAsync(k => k.Id, v => v);

            if (entities.Count != input.Items.Count) NotFoundException(InstanceName);

            foreach (var i in input.Items)
            {
                if (!entities.ContainsKey(i.Id)) NotFoundException(InstanceName);

                i.LastModifierUserId = input.UserId;
                UpdateInstance(i, entities[i.Id]);
            }

            await _repository.BulkUpdateAsync(entities.Values.ToList());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkDeleteAsync(List<Guid> input)
        {
            var entities = await _repository.GetAll().Include(s => s.Location).AsNoTracking().Where(s => input.Contains(s.Id)).ToListAsync();

            if(entities.Count != input.Count) NotFoundException(InstanceName);

            if (entities.Any())
            {
                var locations = entities.Where(s => s.Location != null).Select(s => s.Location).ToList();

                await _repository.BulkDeleteAsync(entities);
                if (locations.Any()) await _locationRepository.BulkDeleteAsync(locations);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ChangeLocationAsync(ChangeContactAddressLocationInput input)
        {
            ValidateSelect(input.LocationId, L("Location"));

            var find = await _locationRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.LocationId.Value);
            if (!find) InvalidException(L("Location"));

            var entity = await GetAsync(input.Id);
            if (entity == null) NotFoundException(InstanceName);

            var locationId = entity.LocationId;

            entity.SetLocation(input.LocationId);
            entity.LastModifierUserId = input.UserId;
            entity.LastModificationTime = Clock.Now;

            await _repository.UpdateAsync(entity);
            if(locationId.HasValue) await _locationManager.DeleteAsync(locationId.Value);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteLocationAsync(IUserEntity<Guid> input)
        {
            var entity = await GetAsync(input.Id);
            if(entity == null) NotFoundException(InstanceName);
            
            if(!entity.LocationId.HasValue) NotFoundException(L("Location"));
            
            var locationId = entity.LocationId.Value;
            
            entity.SetLocation(null);
            entity.LastModifierUserId = input.UserId;
            entity.LastModificationTime = Clock.Now;
            
            await _repository.UpdateAsync(entity);
            await _locationManager.DeleteAsync(locationId);

            return IdentityResult.Success;
        }
    }
}
