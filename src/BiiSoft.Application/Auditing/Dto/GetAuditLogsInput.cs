using System;
using System.Collections.Generic;
using Abp.Extensions;
using Abp.Runtime.Validation;
using BiiSoft.Columns;
using BiiSoft.Dtos;

namespace BiiSoft.Auditing.Dto
{
    public class GetAuditLogsInput : PagedSortFilterInputDto, IShouldNormalize
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string UserName { get; set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string BrowserInfo { get; set; }

        public bool? HasException { get; set; }

        public int? MinExecutionDuration { get; set; }

        public int? MaxExecutionDuration { get; set; }

        public void Normalize()
        {
            if (SortField.IsNullOrWhiteSpace())
            {
                SortField = "ExecutionTime";
                SortMode = Enums.SortMode.DESC;
            }
        }
    }

    public class ExportAuditLogsInput : GetAuditLogsInput
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}