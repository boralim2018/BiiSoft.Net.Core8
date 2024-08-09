using Abp.Application.Services;
using BiiSoft.Branches.Dto;
using BiiSoft.CompanySettings.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CompanySettings
{
    public interface ICompanySettingAppService : IApplicationService
    {        
        Task<CompanySettingDto> GetDetail();
        Task<Guid> CreateOrUpdateProfile(CreateUpdateBranchInputDto input);
        Task<long> CreateOrUpdateGeneralSetting(CreateUpdateCompanyGeneralSettingInputDto input);
        Task<long> CreateOrUpdateAdvanceSetting(CreateUpdateCompanyAdvanceSettingInputDto input);

    }
}
