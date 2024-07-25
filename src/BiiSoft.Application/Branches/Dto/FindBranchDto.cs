using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Branches.Dto
{
    public class FindBranchDto : DefaultNameDto<Guid>
    {
        public string BusinessId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
