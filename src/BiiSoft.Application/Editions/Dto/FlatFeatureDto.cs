using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;

namespace BiiSoft.Editions.Dto
{
    [AutoMapFrom(typeof(Feature))]
    public class FlatFeatureDto
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string DefaultValue { get; set; }

        public FeatureInputTypeDto InputType { get; set; }
    }
}