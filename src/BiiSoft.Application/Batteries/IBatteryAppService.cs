﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Batteries.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Batteries
{
    public interface IBatteryAppService : IApplicationService
    {
        Task<PagedResultDto<BatteryListDto>> GetList(PageBatteryInputDto input);
        Task<Guid> Create(CreateUpdateBatteryInputDto input);
        Task Update(CreateUpdateBatteryInputDto input);
        Task<BatteryDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindBatteryDto> GetDefaultValue();
        Task<PagedResultDto<FindBatteryDto>> Find(PageBatteryInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelBatteryInputDto input);
    }
}
