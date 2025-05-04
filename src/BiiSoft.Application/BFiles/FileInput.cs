using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace BiiSoft.BFiles
{
    public class FileTokenInput
    { 
        public string Token { get; set; }
    }

    public class UpdateFileInput<TPrimary> : EntityDto<TPrimary>
    {
        public string FileId { get; set; }
    }
}
