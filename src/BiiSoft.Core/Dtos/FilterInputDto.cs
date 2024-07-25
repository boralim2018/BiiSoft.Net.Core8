using System;
using System.Collections.Generic;
using System.Text;

namespace BiiSoft.Dtos
{
    public class FilterInputDto
    {
        public string Keyword { get; set; }
    }

    public class FilterInputDto<TPrimary>
    {
        public bool Exclude { get; set; }
        public List<TPrimary> Ids { get; set; }
    }
   
}
