using System;
using System.ComponentModel.DataAnnotations;

namespace BiiSoft.MultiTenancy.Dto
{
    public class UpdateLogoInput
    {
        [Required]
        public Guid? LogoId { get; set; }
    }
}