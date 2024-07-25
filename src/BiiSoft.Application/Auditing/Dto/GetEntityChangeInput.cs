using System;
using Abp.Extensions;
using Abp.Runtime.Validation;
using BiiSoft.Dtos;

namespace BiiSoft.Auditing.Dto
{
    public class GetEntityChangeInput : PagedSortInputDto, IShouldNormalize
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public string EntityTypeFullName { get; set; }

        public void Normalize()
        {
            if (SortField.IsNullOrWhiteSpace())
            {
                SortField = "ChangeTime";
                SortMode = Enums.SortMode.DESC;
            }
        }
    }
}