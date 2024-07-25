using System;
using Abp.Application.Services.Dto;

namespace BiiSoft.Authorization.Users.Dto
{
    public class LinkedUserDto : EntityDto<long>
    {
        public int? TenantId { get; set; }

        public string TenancyName { get; set; }

        public string UserName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public object GetShownLoginName(bool multiTenancyEnabled)
        {
            if (!multiTenancyEnabled)
            {
                return UserName;
            }

            return string.IsNullOrEmpty(TenancyName) ? ".\\" + UserName : TenancyName + "\\" + UserName;
        }
    }
}