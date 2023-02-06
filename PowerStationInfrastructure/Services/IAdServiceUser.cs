using PowerStationDomain.models;

namespace PowerStationInfrastructure.Services;

public interface IAdServiceUser : IAdService<User>
{
    /// <summary>
    /// Sets the enabled boolean to false on the specified user account.
    /// </summary>
    /// <param name="name">The unique identifier of the user.</param>
    /// <returns>True if the account was disabled.</returns>
    Boolean DisableUser(string name);

    /// <summary>
    /// Sets the enabled boolean to true on the specified user account.
    /// </summary>
    /// <param name="name">The unique identifier of the user.</param>
    /// <returns>True if the account was enabled, false otherwise.</returns>
    Boolean EnableUser(string name);

    /// <summary>
    /// This method searches for a user with the specified email, and if found, returns the UserPrincipal object
    /// associated with it.
    /// </summary>
    /// <param name="email">The email address that identifies the user.</param>
    /// <returns>A UserPrincipal object.</returns>
    User? GetUserByEmail(string email);
}