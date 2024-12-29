using AutoMapper;

namespace BiiSoft.Items.Series.Dto
{
    public class ItemSeriesMapProfile : Profile
    {
        public ItemSeriesMapProfile()
        {
            CreateMap<CreateUpdateItemSeriesInputDto, ItemSeries>().ReverseMap();
            CreateMap<ItemSeriesDetailDto, ItemSeries>().ReverseMap();
            CreateMap<FindItemSeriesDto, ItemSeries>().ReverseMap();
        }
    }
}
