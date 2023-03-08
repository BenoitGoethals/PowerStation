using PowerStationDomain.models.AD;
using PowerStationDomain.temp;

namespace PowerStationApplicationCore;

public static class UserBuilder
{
    public static User BuildFQ( User user)
    {
        user.UserPrincipalName= user.Name +user.GivenName;
        user.DisplayName = user.GivenName+ " "+user.MiddleName+ " "+user.Surname;
        user.SamAccountName = user.UserPrincipalName;
        user.Department = "dept";
        return user;
    }
}