using AutoMapper;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class ChartOfAccountMapProfile : Profile
    {
        public ChartOfAccountMapProfile()
        {
            CreateMap<CreateUpdateChartOfAccountInputDto, ChartOfAccount>().ReverseMap();
            CreateMap<ChartOfAccountDetailDto, ChartOfAccount>().ReverseMap();
        }
    }
}
