using Abp.Application.Services.Dto;
using BiiSoft.Enums;

namespace BiiSoft.Dtos
{
    public abstract class PagedInputDto : PagedResultRequestDto, IPagedResultRequest
    {
        public virtual bool UsePagination { get; set; } = true;
        public PagedInputDto()
        {
            MaxResultCount = BiiSoftConsts.DefaultPageSize;
        }
    }

    public abstract class PagedFilterInputDto : PagedInputDto, IPagedResultRequest
    {

        public string Keyword { get; set; }

    }

    public abstract class PagedSortInputDto : PagedInputDto
    {
        public string SortField { get; set; }
        public SortMode SortMode { get; set; } = SortMode.ASC;
        public string GetOrdering()
        {
            return string.IsNullOrWhiteSpace(SortField) ? "" : SortField + " " + SortMode.ToString();
        }

    }

    public abstract class PagedSortFilterInputDto : PagedSortInputDto
    {
        public string Keyword { get; set; }
    }

    public abstract class PagedActiveInputDto : PagedInputDto
    {
        public bool? IsActive { get; set; }
    }

    public abstract class PagedActiveFilterInputDto : PagedFilterInputDto
    {
        public bool? IsActive { get; set; }
    }

    public abstract class PagedActiveSortFilterInputDto : PagedSortFilterInputDto
    {
        public bool? IsActive { get; set; }
    }

    public abstract class PageAuditedSortFilterInputDto: PagedSortFilterInputDto
    {
        public FilterInputDto<long?> Creators { get; set; }
        public FilterInputDto<long?> Modifiers { get; set; }
    }

    public abstract class PageAuditedAcitveSortFilterInputDto : PageAuditedSortFilterInputDto
    {
        public bool? IsActive { get; set; }
    }
}