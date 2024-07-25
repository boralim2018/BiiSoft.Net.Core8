using System.Collections.Generic;
using Abp.Application.Services.Dto;
using BiiSoft.Editions.Dto;

namespace BiiSoft.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}