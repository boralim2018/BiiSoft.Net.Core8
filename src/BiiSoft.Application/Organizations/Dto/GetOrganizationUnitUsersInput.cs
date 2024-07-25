using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using BiiSoft.Dtos;

namespace BiiSoft.Organizations.Dto
{
    public class GetOrganizationUnitUsersInput : PagedSortInputDto, IShouldNormalize
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(SortField))
            {
                SortField = "UserName";
                SortMode = Enums.SortMode.ASC;
            }
        }
    }
}