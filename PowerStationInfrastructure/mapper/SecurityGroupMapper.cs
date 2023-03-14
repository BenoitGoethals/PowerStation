using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PowerStationDomain.models.AD;

namespace PowerStationInfrastructure.mapper
{
    public class SecurityGroupMapper:Profile
    {

        public SecurityGroupMapper()
        {
            CreateMap<GroupPrincipal, SecurityGroup>()
                .ForMember(dest => dest.GroupName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.DistinguishedName, act => act.MapFrom(src => src.DistinguishedName))
                .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Sid, act => act.MapFrom(src => src.Sid))
                .ForMember(dest => dest.ObjectGuid, act => act.MapFrom(src => src.Guid))
                .ForMember(dest => dest.SamAccountName, act => act.MapFrom(src => src.SamAccountName))
                .ForMember(dest => dest.Members,
                    act => act.Ignore())
                .ForMember(dest => dest.UserPrincipalName, act => act.MapFrom(src => src.UserPrincipalName));
            CreateMap<SecurityGroup, GroupPrincipal>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.DistinguishedName, act => act.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Sid, act => act.MapFrom(src => src.Sid))

                .ForMember(dest => dest.Members,
                    act => act.Ignore())
                .ForMember(dest => dest.SamAccountName, act => act.MapFrom(src => src.SamAccountName))
                .ForMember(dest => dest.UserPrincipalName, act => act.MapFrom(src => src.UserPrincipalName)).ConstructUsing(c => new GroupPrincipal(new PrincipalContext(ContextType.Domain, Environment.UserDomainName)));

           

        }




    }

}

