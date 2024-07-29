using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Authorization.Users;
using Abp.Localization;
using Abp.UI.Inputs;
using AutoMapper;
using BiiSoft.Authorization.Roles;
using BiiSoft.ContactInfo;
using BiiSoft.ContactInfo.Dto;
using BiiSoft.Editions;
using BiiSoft.Editions.Dto;
using BiiSoft.Localization.Dto;
using BiiSoft.Roles.Dto;

namespace BiiSoft
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            ////Chat
            //configuration.CreateMap<ChatMessage, ChatMessageDto>();
            //configuration.CreateMap<ChatMessage, ChatMessageExportDto>(); 

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, Edition>().ReverseMap();
            configuration.CreateMap<Edition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();


            ////Payment
            //configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            //configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            //configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            ////Permission
            //configuration.CreateMap<Permission, FlatPermissionDto>();
            //configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            configuration.CreateMap<ContactAddressDto, ContactAddress>().ReverseMap();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();            
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));
            //configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();

            ////Tenant
            //configuration.CreateMap<Tenant, RecentTenant>();
            //configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            //configuration.CreateMap<Tenant, TenantListDto>();
            //configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            //configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            ////User
            //configuration.CreateMap<User, UserEditDto>()
            //    .ForMember(dto => dto.Password, options => options.Ignore())
            //    .ReverseMap()
            //    .ForMember(user => user.Password, options => options.Ignore());
            //configuration.CreateMap<User, UserLoginInfoDto>();
            //configuration.CreateMap<User, UserListDto>();
            //configuration.CreateMap<User, ChatUserDto>();
            //configuration.CreateMap<User, OrganizationUnitUserListDto>();
            //configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            //configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();

            ////AuditLog
            //configuration.CreateMap<AuditLog, AuditLogListDto>();
            //configuration.CreateMap<EntityChange, EntityChangeListDto>();

            ////Friendship
            //configuration.CreateMap<Friendship, FriendDto>();
            //configuration.CreateMap<FriendCacheItem, FriendDto>();

            ////OrganizationUnit
            //configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}