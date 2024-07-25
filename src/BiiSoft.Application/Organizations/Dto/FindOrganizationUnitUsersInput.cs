using BiiSoft.Dtos;

namespace BiiSoft.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedFilterInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
