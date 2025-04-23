using BiiSoft.Dtos;
using System;

namespace BiiSoft.Items.Dto
{
    public class FindItemDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string ALTCode { get; set; }
    }
}
