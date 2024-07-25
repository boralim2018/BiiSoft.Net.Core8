using AutoMapper;

namespace BiiSoft.Currencies.Dto
{
    public class CurrencyMapProfile : Profile
    {
        public CurrencyMapProfile()
        {
            CreateMap<CreateUpdateCurrencyInputDto, Currency>().ReverseMap();
            CreateMap<CurrencyDetailDto, Currency>().ReverseMap();
        }
    }
}
