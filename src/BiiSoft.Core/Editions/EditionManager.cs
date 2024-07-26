using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiiSoft.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = DiamondEditionName;
        public const string StandardEditionName = "Standard";
        public const string StandardEditionDisplayName = "ស្តង់ដា";
        public const string GoldEditionName = "Gold";
        public const string GoldEditionDisplayName = "មាស";
        public const string DiamondEditionName = "Diamond";
        public const string DiamondEditionDisplayName = "ពេជ្រ";

        public EditionManager(
            IRepository<Edition> editionRepository,
            IAbpZeroFeatureValueStore featureValueStore,
            IUnitOfWorkManager unitOfWorkManager) 
            : base(editionRepository, featureValueStore, unitOfWorkManager)
        {

        }

        public async Task<List<Edition>> GetAllAsync()
        {
            return await EditionRepository.GetAll().AsNoTracking().ToListAsync();
        }
    }
}
