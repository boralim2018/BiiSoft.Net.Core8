using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Editions.Dto
{
    public class EditionEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public decimal? MonthlyPrice { get; set; }

        public decimal? AnnualPrice { get; set; }

        public int? TrialDayCount { get; set; }

        public int? WaitingDayAfterExpire { get; set; }

        public int? ExpiringEditionId { get; set; }
    }
}