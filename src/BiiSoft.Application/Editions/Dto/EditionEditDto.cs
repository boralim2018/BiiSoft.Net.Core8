using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Editions.Dto
{
    public class EditionEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string DisplayName { get; set; }

    }
}