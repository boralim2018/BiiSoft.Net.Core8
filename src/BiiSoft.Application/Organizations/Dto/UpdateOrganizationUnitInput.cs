using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Organizations.Dto
{
    public class UpdateOrganizationUnitInput
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(BiiSoftConsts.MaxLengthLongName)]
        public string DisplayName { get; set; }
    }
}