using Abp.Authorization.Users;
using AutoMapper;
using BiiSoft.Authorization.Users;
using BiiSoft.Users.Profiles.Dto;

namespace BiiSoft.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<User, CurrentUserProfileEditDto>();
            CreateMap<CurrentUserProfileEditDto, User>();
            CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
        }
    }
}
