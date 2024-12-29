﻿using BiiSoft.Columns;
using BiiSoft.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Items.Series.Dto
{
    public class PageItemSeriesInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemSeriesInputDto : PageItemSeriesInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
