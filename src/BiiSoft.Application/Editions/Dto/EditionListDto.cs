using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace BiiSoft.Editions.Dto
{
    public class EditionListDto : EntityDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}