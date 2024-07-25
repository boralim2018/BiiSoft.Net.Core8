using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Auditing.Dto;
using BiiSoft.Dtos;

namespace BiiSoft.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter 
    {
        FileDto ExportFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
