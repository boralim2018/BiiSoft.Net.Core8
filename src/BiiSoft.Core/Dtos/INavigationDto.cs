using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Dtos
{
    public interface INavigationDto<TPrimaryKey>: IEntityDto<TPrimaryKey> where TPrimaryKey : struct
    {
        TPrimaryKey? FirstId { get; set; }
        TPrimaryKey? NextId { get; set; }
        TPrimaryKey? PreviousId { get; set; }
        TPrimaryKey? LastId { get; set; }
    }
}
