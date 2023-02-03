using System.DirectoryServices.AccountManagement;
using CsvHelper;
using PowerStationDomain.models;
using GroupScope = PowerStationDomain.models.GroupScope;


namespace PowerStationInfrastructure.helpers;

public static class ConvertorExtension
{
    public static System.DirectoryServices.AccountManagement.GroupScope ConvertToGroupPrincipal(this PowerStationDomain.models.GroupScope group)
    {
        if (group == GroupScope.Local) return System.DirectoryServices.AccountManagement.GroupScope.Local;
        if (group == GroupScope.Global) return System.DirectoryServices.AccountManagement.GroupScope.Global;
        if (group == GroupScope.Universal) return System.DirectoryServices.AccountManagement.GroupScope.Universal;
        return System.DirectoryServices.AccountManagement.GroupScope.Local;
    }
}