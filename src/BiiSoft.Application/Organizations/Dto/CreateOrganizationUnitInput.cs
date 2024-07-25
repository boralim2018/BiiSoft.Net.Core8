using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Organizations.Dto
{
    public class CreateOrganizationUnitInput
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(BiiSoftConsts.MaxLengthLongName)]
        public string DisplayName { get; set; } 
    }
}