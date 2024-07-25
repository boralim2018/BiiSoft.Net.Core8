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

namespace BiiSoft.ContactInfo
{
    public abstract class ContactAddressBaseManager<TEntity, TPrimaryKey> : BiiSoftValidateServiceBase<TEntity, TPrimaryKey>, IContactAddressBaseManager<TEntity, TPrimaryKey> 
        where TEntity : ContactAddressBase<TPrimaryKey>
    {

        protected readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        protected readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        protected readonly IBiiSoftRepository<KhanDistrict, Guid> _khanDistrictRepository;
        protected readonly IBiiSoftRepository<SangkatCommune, Guid> _sangkatCommuneRepository;
        protected readonly IBiiSoftRepository<Village, Guid> _villageRepository;
        protected readonly IBiiSoftRepository<Location, Guid> _locationRepository;
        public ContactAddressBaseManager(
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<Village, Guid> villageRepository,
            IBiiSoftRepository<Location, Guid> locationRepository,
            IBiiSoftRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
            _countryRepository = countryRepository;
            _cityProvinceRepository = cityProvinceRepository;
            _khanDistrictRepository = khanDistrictRepository;
            _sangkatCommuneRepository = sangkatCommuneRepository;
            _villageRepository = villageRepository;
            _locationRepository = locationRepository;
        }

        protected virtual void ValidateInput(TEntity input)
        {
            ValidateSelect(input.CountryId, L("Country"));
        }

        protected override async Task ValidateInputAsync(TEntity input)
        {
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

        protected virtual void BulkValidate(List<TEntity> input)
        {
            var find = input.Any(s => s.CountryId.IsNullOrEmpty());
            if (find) SelectException(L("Country"));
        }

        public async virtual Task BulkValidateAsync(List<TEntity> input)
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

        public async Task<IdentityResult> BulkInsertAsync(int? tenantId, long userId, List<TEntity> input)
        {
            await BulkValidateAsync(input);

            var entities = input.Select(s => {
                var e = CreateInstance(tenantId, userId, s);
                s.Id = e.Id;
                return e;
            }).ToList();

            await _repository.BulkInsertAsync(entities);          
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkUpdateAsync(long userId, List<TEntity> input)
        {
            await BulkValidateAsync(input);

            var entities = await _repository.GetAll().AsNoTracking().Where(s => input.Any(r => r.Id.Equals(s.Id))).ToDictionaryAsync(k => k.Id, v => v);

            foreach (var i in input)
            {
                if (!entities.ContainsKey(i.Id)) NotFoundException();
                UpdateInstance(userId, i, entities[i.Id]);
            }

            await _repository.BulkUpdateAsync(entities.Values.ToList());
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkDeleteAsync(List<TPrimaryKey> input)
        {
            var entities = await _repository.GetAll().AsNoTracking().Where(s => input.Contains(s.Id)).ToListAsync();
                       
            if(entities.Any()) await _repository.BulkDeleteAsync(entities);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ChangeLocationAsync(long userId, Guid? locationId, TPrimaryKey id)
        {
            ValidateSelect(locationId, L("Location"));

            var find = await _locationRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == locationId);
            if (!find) InvalidException(L("Location"));

            var entity = await GetAsync(id, false);
            ValidateFind(entity);

            entity.SetLocation(locationId);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = Clock.Now;

            await _repository.UpdateAsync(entity);

            return IdentityResult.Success;
        }
    }
}
