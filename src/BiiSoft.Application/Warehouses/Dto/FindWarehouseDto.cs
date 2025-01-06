using BiiSoft.Dtos;
using System;

namespace BiiSoft.Warehouses.Dto
{
    public class FindWarehouseDto : NameActiveDto<Guid>
    {
        public string Code { get; set; }
    }
}
