using System.DirectoryServices.AccountManagement;
using AutoMapper;
using PowerStationDomain.models;

namespace PowerStationInfrastructure.mapper;

public class ComputerMapper:Profile
{
    public ComputerMapper()
    {
        CreateMap<ComputerPrincipal, Computer>()
            .ForMember(dest => dest.ServicePrincipalNames, act => act.MapFrom(src => src.ServicePrincipalNames))
            .ForMember(dest => dest.DistinguishedName, act => act.MapFrom(src => src.DistinguishedName))
            .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.DisplayName))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.Sid, act => act.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Enabled, act => act.MapFrom(src => src.Enabled))

            .ForMember(dest => dest.AccountExpirationDate, act => act.MapFrom(src => src.AccountExpirationDate))
            .ForMember(dest => dest.AccountLockouttime, act => act.MapFrom(src => src.AccountLockoutTime))

            .ForMember(dest => dest.BadLogonCount, act => act.MapFrom(src => src.BadLogonCount))
            .ForMember(dest => dest.DelegationPermitted, act => act.MapFrom(src => src.DelegationPermitted))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.SamAccountName, act => act.MapFrom(src => src.SamAccountName))
            .ForMember(dest => dest.UserCannotChangePassword, act => act.MapFrom(src => src.UserCannotChangePassword))
            .ForMember(dest => dest.LastBadPasswordAttempt, act => act.MapFrom(src => src.LastBadPasswordAttempt))
            .ForMember(dest => dest.UserPrincipalName, act => act.MapFrom(src => src.UserPrincipalName));
            

        CreateMap<Computer, ComputerPrincipal>()
            .ForMember(dest => dest.ServicePrincipalNames, act => act.MapFrom(src => src.ServicePrincipalNames))
            .ForMember(dest => dest.DistinguishedName, act => act.MapFrom(src => src.DistinguishedName))
            .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.DisplayName))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.Sid, act => act.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Enabled, act => act.MapFrom(src => src.Enabled))

            .ForMember(dest => dest.AccountExpirationDate, act => act.MapFrom(src => src.AccountExpirationDate))
            .ForMember(dest => dest.AccountLockoutTime, act => act.MapFrom(src => src.AccountLockouttime))

            .ForMember(dest => dest.BadLogonCount, act => act.MapFrom(src => src.BadLogonCount))
            .ForMember(dest => dest.DelegationPermitted, act => act.MapFrom(src => src.DelegationPermitted))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.SamAccountName, act => act.MapFrom(src => src.SamAccountName))
            .ForMember(dest => dest.UserCannotChangePassword, act => act.MapFrom(src => src.UserCannotChangePassword))
            .ForMember(dest => dest.LastBadPasswordAttempt, act => act.MapFrom(src => src.LastBadPasswordAttempt))
            .ForMember(dest => dest.UserPrincipalName, act => act.MapFrom(src => src.UserPrincipalName));




    }
}
