using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using BiiSoft.Features;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiiSoft.Authorization.Users
{
    public class UserPolicy : AbpServiceBase, IUserPolicy
    {
        private readonly IFeatureChecker _featureChecker;
        private readonly IRepository<User, long> _userRepository;

        public UserPolicy(IFeatureChecker featureChecker,IRepository<User, long> userRepository)
        {
            _featureChecker = featureChecker;
            _userRepository = userRepository;
        }

        public async Task CheckMaxUserCountAsync(int tenantId)
        {
            var maxUserCount = (await _featureChecker.GetValueAsync(tenantId, AppFeatures.MaxUserCount)).To<int>();
            if (maxUserCount <= 0)
            {
                return;
            }

            var currentUserCount = await _userRepository.GetAll().AsNoTracking().Where(s => !s.IsDeactivate).CountAsync();
            if (currentUserCount >= maxUserCount)
            {
                throw new UserFriendlyException(L("MaximumUserCount_Error_Message"), L("MaximumUserCount_Error_Detail", maxUserCount));
            }
        }
    }
}