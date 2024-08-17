using AutoMapper;
using BiiSoft.Branches;

namespace BiiSoft.CompanySettings.Dto
{
    public class CompanySettingMapProfile : Profile
    {
        public CompanySettingMapProfile()
        {
            CreateMap<CreateUpdateCompanyGeneralSettingInputDto, CompanyGeneralSetting>().ReverseMap();
            CreateMap<CompanyGeneralSettingDto, CompanyGeneralSetting>().ReverseMap();
            CreateMap<CreateUpdateCompanyAdvanceSettingInputDto, CompanyAdvanceSetting>().ReverseMap();
            CreateMap<CompanyAdvanceSettingDto, CompanyAdvanceSetting>().ReverseMap();
            CreateMap<CreateUpdateTransactionNoSettingInputDto, TransactionNoSetting>().ReverseMap();
            CreateMap<TransactionNoSettingDto, TransactionNoSetting>().ReverseMap();
        }
    }
}
