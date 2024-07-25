using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace BiiSoft.Authorization.Users.Dto
{
    public class LinkToUserInput
    {
        public string TenancyName { get; set; }

        [Required]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [DisableAuditing]
        public string Password { get; set; }
    }
}