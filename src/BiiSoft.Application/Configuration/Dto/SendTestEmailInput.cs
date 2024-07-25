using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace BiiSoft.Configuration.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        public string CompanyName { get; set; }
    }
}