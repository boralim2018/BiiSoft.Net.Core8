using Abp.Collections.Extensions;
using Abp.Timing;
using BiiSoft.ContactInfo;
using BiiSoft.Locations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class BranchContactAddressManager : ContactAddressBaseManager<BranchContactAddress, Guid>, IBranchContactAddressManager
    {
        IBiiSoftRepository<Branch, Guid> _branchRepository;
        public BranchContactAddressManager(
            IBiiSoftRepository<Branch, Guid> branchRepository,
            IBiiSoftRepository<Country, Guid> countryRepository, 
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository, 
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository, 
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository, 
            IBiiSoftRepository<Village, Guid> villageRepository, 
            IBiiSoftRepository<Location, Guid> locationRepository, 
            IBiiSoftRepository<BranchContactAddress, Guid> repository) : 
            base(countryRepository, cityProvinceRepository, khanDistrictRepository, sangkatCommuneRepository, villageRepository, locationRepository, repository)
        {
            _branchRepository = branchRepository;
        }

        protected override string InstanceName => throw new NotImplementedException();

        protected override void ValidateInput(BranchContactAddress input)
        {
            ValidateSelect(input.BranchId, L("Branch"));
            base.ValidateInput(input);
        }

        protected override async Task ValidateInputAsync(BranchContactAddress input)
        {
            var find = await _branchRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.BranchId);
            if (!find) InvalidException(L("Branch"));
                
            await base.ValidateInputAsync(input);
        }

        protected override void BulkValidate(List<BranchContactAddress> input)
        {
            var find = input.Any(s => s.BranchId == Guid.Empty);
            if (find) SelectException(L("Branch"));

            base.BulkValidate(input);
        }

        public override async Task BulkValidateAsync(List<BranchContactAddress> input)
        {
            await base.BulkValidateAsync(input);

            var branchHash = input.GroupBy(s => s.BranchId).Select(s => s.Key).ToHashSet();
            var findBranch = await _branchRepository.GetAll().AsNoTracking().CountAsync(s => branchHash.Contains(s.Id)) == branchHash.Count;
            if (!findBranch) InvalidException(L("Branch"));
        }

        protected override BranchContactAddress CreateInstance(int? tenantId, long userId, BranchContactAddress input)
        {
            var entity = BranchContactAddress.Create(tenantId.Value, userId, input.BranchId, input.CountryId, input.CityProvinceId, input.KhanDistrictId, input.SangkatCommuneId, input.VillageId, input.LocationId, input.PostalCode, input.Street, input.HouseNo);
            entity.SetDefault(input.IsDefault);

            return entity;
        }

        protected override void UpdateInstance(long userId, BranchContactAddress input, BranchContactAddress entity)
        {
            entity.Update(userId, input.BranchId, input.CountryId, input.CityProvinceId, input.KhanDistrictId, input.SangkatCommuneId, input.VillageId, input.LocationId, input.PostalCode, input.Street, input.HouseNo);
        }

        public async Task<IdentityResult> SetAsDefaultAsync(long userId, Guid id)
        {
            var entity = await GetAsync(id);
            ValidateFind(entity);
            var modificationTime = Clock.Now;

            var otherDefault = await _repository.GetAll().Where(s => !s.Id.Equals(id) && s.BranchId == entity.BranchId && s.IsDefault).AsNoTracking().ToListAsync();
            foreach (var d in otherDefault)
            {
                d.SetDefault(false);
                d.LastModifierUserId = userId;
                d.LastModificationTime = modificationTime;
            }

            entity.SetDefault(true);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = modificationTime;

            otherDefault.Add(entity);

            await _repository.BulkUpdateAsync(otherDefault);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> BulkSyncAsync(int? tenantId, long userId, Guid branchId, List<BranchContactAddress> input)
        {
            if(!input.IsNullOrEmpty()) await BulkValidateAsync(input);

            var addresses = await _repository.GetAll().AsNoTracking().Where(s => s.BranchId == branchId).ToListAsync();

            var addAddress = input.Where(s => s.Id == Guid.Empty).ToList();
            var updateAddress = input.Where(s => s.Id != Guid.Empty).ToList();
            var deleteAddress = addAddress.Where(s => !updateAddress.Any(r => r.Id == s.Id)).ToList();

            var find = updateAddress.All(s => addresses.Any(r => r.Id == s.Id));
            if (!find) NotFoundException();

            if (addAddress.Any())
            {
                var entities = addAddress.Select(s => {
                    var e = CreateInstance(tenantId, userId, s);
                    s.Id = e.Id;
                    return e;
                }).ToList();

                await _repository.BulkInsertAsync(entities);
            }
           
            if(updateAddress.Any())
            {
                var entities = new List<BranchContactAddress>();
                foreach (var i in updateAddress)
                {
                    var entity = addresses.FirstOrDefault(s => s.Id == i.Id);
                    if(entity == null) NotFoundException();

                    UpdateInstance(userId, i, entity);
                    entities.Add(entity);
                }

                await _repository.BulkUpdateAsync(entities.ToList());
            }
           
            if(deleteAddress.Any()) await _repository.BulkDeleteAsync(deleteAddress);

            return IdentityResult.Success;
        }
    }
}
