using Abp.Runtime.Validation;
using BiiSoft.Dtos;

namespace BiiSoft.Authorization.Users.Dto
{
    public class GetLinkedUsersInput : PagedSortInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if (string.IsNullOrEmpty(SortField))
            {
                SortField = "Username";
            }
        }
    }
}