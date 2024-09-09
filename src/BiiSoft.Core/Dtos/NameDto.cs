using Abp.Application.Services.Dto;
using System;

namespace BiiSoft.Dtos
{
    public abstract class NameDto<TPrimary> : EntityDto<TPrimary>, INameDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public abstract class NameDto : NameDto<int>
    {

    }

    public abstract class NameActiveDto<TPrimary> : NameDto<TPrimary>, IActiveDto
    {
        public bool IsActive { get; set; }
    }

    public abstract class DefaultNameDto<TPrimary> : NameActiveDto<TPrimary>, IDefaultDto
    {
        public bool IsDefault { get; set; }
    }

    public abstract class CanModifyNameDto<TPrimary> : NameActiveDto<TPrimary>, ICanModifyDto
    {
        public bool CannotEdit { get; set; }
        public bool CannotDelete { get; set; }
    }

    public abstract class CanModifyDefaultNameDto<TPrimary> : CanModifyNameDto<TPrimary>, IDefaultDto
    {
        public bool IsDefault { get; set; }
    }

}
