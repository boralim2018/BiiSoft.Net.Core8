using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Auditing.Dto;
using BiiSoft.BFiles;

namespace BiiSoft.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<ExportFileOutput> ExportExcel(ExportAuditLogsInput input);

        //Task<PagedResultDto<EntityChangeListDto>> GetEntityChanges(GetEntityChangeInput input);

        //Task<FileDto> GetEntityChangesToExcel(GetEntityChangeInput input);

        //Task<List<EntityPropertyChangeDto>> GetEntityPropertyChanges(long entityChangeId);

        //List<NameValueDto> GetEntityHistoryObjectTypes();
    }
}