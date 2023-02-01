using System.DirectoryServices.AccountManagement;
using AutoMapper;
using PowerStationDomain.models;

namespace PowerStationInfrastructure.mapper;

public class UserExtensionMapper
{
    private MapperConfiguration mapperUser = new MapperConfiguration(cfg =>
        cfg.CreateMap<User, UserPrincipal>()
            .ForMember(dest => dest.GivenName, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.EmailAddress, act => act.MapFrom(src => src.EmailAddress))
            .ForMember(dest => dest.MiddleName, act => act.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.EmployeeId, act => act.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.VoiceTelephoneNumber, act => act.MapFrom(src => src.VoiceTelephoneNumber))
            .ForMember(dest => dest.UserPrincipalName, act => act.MapFrom(src => src.UserPrincipalName))
            .ForMember(dest => dest.AccountExpirationDate, act => act.MapFrom(src => src.AccountExpirationData))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.AccountLockoutTime, act => act.MapFrom(src => src.AccountLockoutTime))
            .ForMember(dest => dest.AllowReversiblePasswordEncryption, act => act.MapFrom(src => src.AllowReversiblePasswordEncryption))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.Enabled, act => act.MapFrom(src => src.Enabled))
            .ForMember(dest => dest.BadLogonCount, act => act.MapFrom(src => src.BadLogonCount))
            .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.DisplayName))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ForMember(dest => dest.HomeDirectory, act => act.MapFrom(src => src.HomeDirectory))
            .ForMember(dest => dest.HomeDrive, act => act.MapFrom(src => src.HomeDrive))
            .ForMember(dest => dest.SamAccountName, act => act.MapFrom(src => src.SamAccountName))
            .ForMember(dest => dest.Sid, act => act.MapFrom(src => src.Sid))
            .ForMember(dest => dest.UserCannotChangePassword, act => act.MapFrom(src => src.Sid))
            .ForMember(dest => dest.SmartcardLogonRequired, act => act.MapFrom(src => src.SmartCardLogonRequired))
            .ForMember(dest => dest.ScriptPath, act => act.MapFrom(src => src.ScriptPath))
            .ForMember(dest => dest.AccountExpirationDate, act => act.MapFrom(src => src.AccountExpirationData))
            .ForMember(dest => dest.PasswordNeverExpires, act => act.MapFrom(src => src.Company))

            .ForMember(dest => dest.PasswordNotRequired, act => act.MapFrom(src => src.Sid))

            .ForMember(dest => dest.PermittedLogonTimes, act => act.MapFrom(src => src))

            .ForMember(dest => dest.Sid, act => act.MapFrom(src => src.Sid))

            .ForMember(dest => dest.DelegationPermitted, act => act.MapFrom(src => src.DelegationPermitted))
            .ForMember(dest => dest.Surname, act => act.MapFrom(src => src.Surname)));
}