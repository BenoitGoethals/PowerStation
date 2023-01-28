using PowerStationDomain.Domain;

namespace PowerStationApplicationCore.Interfaces;

public interface IUserService
{
    void AddAdUser(User adUser);
    void Remove(User adUser);

    List<User> AllUsers();

}