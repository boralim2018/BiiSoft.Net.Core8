using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using BiiSoft.Auditing.Dto;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Users;
using BiiSoft.Extensions;
using System;
using Abp.Collections.Extensions;
using OfficeOpenXml;
using BiiSoft.Folders;
using Abp.UI;
using BiiSoft.BFiles;
using BiiSoft.FileStorages;
using BiiSoft.Columns;
using Abp.EntityFrameworkCore.Repositories;

namespace BiiSoft.Auditing
{
    [DisableAuditing]
    [AbpAuthorize(PermissionNames.Pages_Administrations_AuditLogs)]
    public class AuditLogAppService : BiiSoftAppServiceBase, IAuditLogAppService
    {
        private readonly IBiiSoftRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly INamespaceStripper _namespaceStripper;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;

        //IAuditLogListExcelExporter _auditLogListExcelExporter,
        //private readonly IAbpStartupConfiguration _abpStartupConfiguration;
        //private readonly IRepository<EntityChange, long> _entityChangeRepository;
        //private readonly IRepository<EntityChangeSet, long> _entityChangeSetRepository;
        //private readonly IRepository<EntityPropertyChange, long> _entityPropertyChangeRepository;

        public AuditLogAppService(
            //IAuditLogListExcelExporter auditLogListExcelExporter,
            //IRepository<EntityChange, long> entityChangeRepository,
            //IRepository<EntityChangeSet, long> entityChangeSetRepository,
            //IRepository<EntityPropertyChange, long> entityPropertyChangeRepository,
            //IAbpStartupConfiguration abpStartupConfiguration,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            IRepository<User, long> userRepository,
            INamespaceStripper namespaceStripper,
            IBiiSoftRepository<AuditLog, long> auditLogRepository
            )
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
            _namespaceStripper = namespaceStripper;
            _appFolders = appFolders;
            _fileStorageManager = fileStorageManager;

            //_auditLogListExcelExporter = auditLogListExcelExporter;
            //_entityChangeRepository = entityChangeRepository;
            //_entityChangeSetRepository = entityChangeSetRepository;
            //_entityPropertyChangeRepository = entityPropertyChangeRepository;
            //_abpStartupConfiguration = abpStartupConfiguration;
        }

        #region audit logs

        [AbpAuthorize(PermissionNames.Pages_Administrations_AuditLogs)]
        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input)
        {
            return await GetListHelper(input);
        }
        private async Task<PagedResultDto<AuditLogListDto>> GetListHelper(GetAuditLogsInput input)
        {
            var tenantZone = "SE Asia Standard Time";
            var startDate = input.StartDate.HasValue ? input.StartDate.Value.StartDateZone(tenantZone) : (DateTime?)null;
            var endDate = input.EndDate.HasValue ? input.EndDate.Value.EndDateZone(tenantZone) : (DateTime?)null;

            var query = from auditLog in _auditLogRepository.GetAll()
                            .WhereIf(startDate.HasValue, s => s.ExecutionTime >= startDate.Value)
                            .WhereIf(endDate.HasValue, s => s.ExecutionTime <= endDate.Value)
                            .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(), s => s.ServiceName.ToLower().Contains(input.ServiceName.ToLower()))
                            .WhereIf(!input.MethodName.IsNullOrWhiteSpace(), s => s.MethodName.ToLower().Contains(input.MethodName.ToLower()))
                            .WhereIf(!input.BrowserInfo.IsNullOrWhiteSpace(), s => s.BrowserInfo.ToLower().Contains(input.BrowserInfo.ToLower()))
                            .WhereIf(input.HasException.HasValue, s => (input.HasException.Value && s.Exception != null && s.Exception != "") ||
                                                                       (!input.HasException.Value && (s.Exception == null || s.Exception == "")))
                            .WhereIf(input.MinExecutionDuration.HasValue && input.MinExecutionDuration > 0, s => s.ExecutionDuration >= input.MinExecutionDuration.Value)
                            .WhereIf(input.MaxExecutionDuration.HasValue && input.MaxExecutionDuration > 0, s => s.ExecutionDuration <= input.MaxExecutionDuration.Value)
                            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.ServiceName.ToLower().Contains(input.Keyword.ToLower()) ||
                                                                               s.MethodName.ToLower().Contains(input.Keyword.ToLower()) ||
                                                                               s.BrowserInfo.ToLower().Contains(input.Keyword.ToLower()))
                            .AsNoTracking()
                        join user in _userRepository.GetAll()
                            .AsNoTracking()
                        on auditLog.UserId equals user.Id into userJoin
                        from joinedUser in userJoin.DefaultIfEmpty()
                        where input.UserName.IsNullOrWhiteSpace() || (joinedUser != null && joinedUser.UserName.ToLower().Contains(input.UserName.ToLower()))
                        select new AuditLogListDto
                        {
                            BrowserInfo = auditLog.BrowserInfo,
                            ClientIpAddress = auditLog.ClientIpAddress,
                            ClientName = auditLog.ClientName,
                            CustomData = auditLog.CustomData,
                            Exception = auditLog.Exception,
                            ExecutionDuration = auditLog.ExecutionDuration,
                            ExecutionTime = auditLog.ExecutionTime,
                            Id = auditLog.Id,
                            ImpersonatorTenantId = auditLog.ImpersonatorTenantId,
                            ImpersonatorUserId = auditLog.ImpersonatorUserId,
                            MethodName = auditLog.MethodName,
                            Parameters = auditLog.Parameters,
                            ServiceName = auditLog.ServiceName,
                            UserId = auditLog.UserId,
                            UserName = joinedUser == null ? "" : joinedUser.UserName,
                            Success = auditLog.Exception.IsNullOrWhiteSpace()
                        };

            var resultCount = await query.CountAsync();
            var results = new List<AuditLogListDto>();

            if (resultCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                results = await query.ToListAsync();

                results.AsParallel().ForAll(s =>
                {
                    s.ServiceName = _namespaceStripper.StripNameSpace(s.ServiceName);
                });

            }

            return new PagedResultDto<AuditLogListDto>(resultCount, results);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_AuditLogs_ExportExcel)]
        public async Task<ExportFileOutput> ExportExcel(ExportAuditLogsInput input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            var result = new ExportFileOutput
            {
                FileName = "AuditLogs.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            var listResult = await GetListHelper(input);

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = input.Columns.
                    Where(s => s.Visible)
                    .OrderBy(s => s.Index)
                    .ThenBy(s => s.ColumnTitle)
                    .ToList();

                //foreach (var i in displayColumns)
                //{
                //    ws.AddTextToCell(rowTableHeader, colHeaderTable, i.ColumnTitle, true);
                //    if (i.Width > 0) ws.Column(colHeaderTable).Width = i.Width.PixcelToInches();

                //    colHeaderTable += 1;
                //}
                #endregion Row 1

                var rowIndex = rowTableHeader + 1;
                foreach (var row in listResult.Items)
                {
                    var colIndex = 1;
                    foreach (var col in displayColumns)
                    {
                        var value = row.GetType().GetProperty(col.ColumnName.ToPascalCase()).GetValue(row);

                        if(col.ColumnType == ColumnType.Bool)
                        {
                            ws.AddTextToCell(rowIndex, colIndex, Convert.ToBoolean(value) ? "Success" : "Error");
                        }
                        else
                        {
                            col.WriteCell(ws, rowIndex, colIndex, value);
                        }

                        colIndex++;
                    }
                    rowIndex++;
                }

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, rowIndex - 1);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
        }


        #endregion

        #region entity changes 
        //public List<NameValueDto> GetEntityHistoryObjectTypes()
        //{
        //    var entityHistoryObjectTypes = new List<NameValueDto>();
        //    var entityHistoryConfig = _abpStartupConfiguration.GetCustomConfig();

        //    if (AbpSession.TenantId == null)
        //    {
        //        entityHistoryConfig = entityHistoryConfig
        //            .Where(c => BiiSoftEntityHistoryHelper.HostSideTrackedTypes.Select(x=>x.FullName).Contains(c.Value.ToString()))
        //            .ToDictionary(key => key.Key, value => value.Value);
        //    }
        //    else
        //    {
        //        entityHistoryConfig = entityHistoryConfig
        //            .Where(c => BiiSoftEntityHistoryHelper.TenantSideTrackedTypes.Select(x => x.FullName).Contains(c.Value.ToString()))
        //            .ToDictionary(key => key.Key, value => value.Value);
        //    }

        //    foreach (var config in entityHistoryConfig)
        //    {
        //        entityHistoryObjectTypes.Add(new NameValueDto(config.Key, config.Value.ToString()));
        //    }

        //    return entityHistoryObjectTypes;
        //}

        //public async Task<PagedResultDto<EntityChangeListDto>> GetEntityChanges(GetEntityChangeInput input)
        //{
        //    var query = CreateEntityChangesAndUsersQuery(input);

        //    var resultCount = await query.CountAsync();
        //    var results = await query
        //        .OrderBy(input.Sorting)
        //        .PageBy(input)
        //        .ToListAsync();

        //    var entityChangeListDtos = ConvertToEntityChangeListDtos(results);

        //    return new PagedResultDto<EntityChangeListDto>(resultCount, entityChangeListDtos);
        //}

        //public async Task<FileDto> GetEntityChangesToExcel(GetEntityChangeInput input)
        //{
        //    var entityChanges = await CreateEntityChangesAndUsersQuery(input)
        //        .AsNoTracking()
        //        .OrderByDescending(ec => ec.EntityChange.EntityChangeSetId)
        //        .ThenByDescending(ec => ec.EntityChange.ChangeTime)
        //        .ToListAsync();

        //    var entityChangeListDtos = ConvertToEntityChangeListDtos(entityChanges);

        //    //return _auditLogListExcelExporter.ExportToFile(entityChangeListDtos);

        //}

        //public async Task<List<EntityPropertyChangeDto>> GetEntityPropertyChanges(long entityChangeId)
        //{
        //    var entityPropertyChanges = (await _entityPropertyChangeRepository.GetAllListAsync())
        //        .Where(epc => epc.EntityChangeId == entityChangeId);

        //    return ObjectMapper.Map<List<EntityPropertyChangeDto>>(entityPropertyChanges);
        //}

        //private List<EntityChangeListDto> ConvertToEntityChangeListDtos(List<EntityChangeAndUser> results)
        //{
        //    return results.Select(
        //        result =>
        //        {
        //            var entityChangeListDto = ObjectMapper.Map<EntityChangeListDto>(result.EntityChange);
        //            entityChangeListDto.UserName = result.User?.UserName;
        //            return entityChangeListDto;
        //        }).ToList();
        //}

        //private IQueryable<EntityChangeAndUser> CreateEntityChangesAndUsersQuery(GetEntityChangeInput input)
        //{
        //    var query = from entityChangeSet in _entityChangeSetRepository.GetAll()
        //                join entityChange in _entityChangeRepository.GetAll() on entityChangeSet.Id equals entityChange.EntityChangeSetId
        //                join user in _userRepository.GetAll() on entityChangeSet.UserId equals user.Id
        //                where entityChange.ChangeTime >= input.StartDate && entityChange.ChangeTime <= input.EndDate
        //                select new EntityChangeAndUser
        //                {
        //                    EntityChange = entityChange,
        //                    User = user
        //                };

        //    query = query
        //        .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
        //        .WhereIf(!input.EntityTypeFullName.IsNullOrWhiteSpace(), item => item.EntityChange.EntityTypeFullName.Contains(input.EntityTypeFullName));

        //    return query;
        //}
        #endregion
    }
}
