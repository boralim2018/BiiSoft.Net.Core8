using Abp.Application.Services.Dto;
using BiiSoft.Enums;

namespace BiiSoft.Dtos
{
    public class PagedInputDto : PagedResultRequestDto, IPagedResultRequest
    {
        public virtual bool UsePagination { get; set; } = true;
        public PagedInputDto()
        {
            MaxResultCount = BiiSoftConsts.DefaultPageSize;
        }
    }

    public class PagedFilterInputDto : PagedInputDto, IPagedResultRequest
    {

        public string Keyword { get; set; }

    }

    public class PagedSortInputDto : PagedInputDto
    {
        public string SortField { get; set; }
        public SortMode SortMode { get; set; } = SortMode.ASC;
        public string GetOrdering() => string.IsNullOrWhiteSpace(SortField) ? "" : SortField + " " + SortMode.ToString();

    }

    public class PagedSortFilterInputDto : PagedSortInputDto
    {
        public string Keyword { get; set; }
    }

    public class PagedActiveInputDto : PagedInputDto
    {
        public bool? IsActive { get; set; }
    }

    public class PagedActiveFilterInputDto : PagedFilterInputDto
    {
        public bool? IsActive { get; set; }
    }

    public class PagedActiveSortFilterInputDto : PagedSortFilterInputDto
    {
        public bool? IsActive { get; set; }
    }

    public class PageAuditedSortFilterInputDto: PagedSortFilterInputDto
    {
        public FilterInputDto<long?> Creators { get; set; }
        public FilterInputDto<long?> Modifiers { get; set; }
    }

    public class PageAuditedAcitveSortFilterInputDto : PageAuditedSortFilterInputDto
    {
        public bool? IsActive { get; set; }
    }
}