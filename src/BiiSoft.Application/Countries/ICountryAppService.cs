﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.Countries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Countries
{
    public interface ICountryAppService : IApplicationService
    {
        Task<PagedResultDto<CountryListDto>> GetList(PageCountryInputDto input);
        Task<Guid> Create(CreateUpdateCountryInputDto input);
        Task Update(CreateUpdateCountryInputDto input);
        Task<CountryDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindCountryDto>> Find(PageCountryInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelCountryInputDto input);
    }
}
