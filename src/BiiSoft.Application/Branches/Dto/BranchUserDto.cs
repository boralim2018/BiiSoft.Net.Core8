using System;

namespace BiiSoft.Branches.Dto
{
    public class BranchUserDto 
    {
        public Guid? Id { get; set; }
        public long MemberId { get; set; }
        public string UserName { get; set; }

    }
}
