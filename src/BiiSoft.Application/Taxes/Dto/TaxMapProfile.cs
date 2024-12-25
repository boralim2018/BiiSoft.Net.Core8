using AutoMapper;
using BiiSoft.Taxes;

namespace BiiSoft.Taxes.Dto
{
    public class TaxMapProfile : Profile
    {
        public TaxMapProfile()
        {
            CreateMap<CreateUpdateTaxInputDto, Tax>().ReverseMap();
            CreateMap<TaxDetailDto, Tax>().ReverseMap();
        }
    }
}
