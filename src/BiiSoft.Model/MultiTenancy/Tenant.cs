using Abp.MultiTenancy;
using BiiSoft.Authorization.Users;
using System;

namespace BiiSoft.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Guid? LogoId { get; protected set; }
        public void SetLogo(Guid? logoId) => LogoId = logoId;

        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
