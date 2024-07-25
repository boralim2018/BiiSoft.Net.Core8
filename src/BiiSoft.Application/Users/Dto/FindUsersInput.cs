using BiiSoft.Dtos;

namespace BiiSoft.Users.Dto
{
    public class FindUsersInput : PagedActiveSortFilterInputDto
    {
        public int? TenantId { get; set; }
    }
}