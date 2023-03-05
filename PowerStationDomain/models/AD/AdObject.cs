using PowerStationDomain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PowerStationDomain.models.AD;

public abstract class AdObject : IAdObject
{
    private readonly List<AdObject?> _adObjects = new();

    public string? Sid { get; set; } = default;
    public string? ObjectClass { get; set; } = default;
    public Guid ObjectGuid { get; set; } = Guid.NewGuid();

    [MaxLength(50, ErrorMessage = "The  Description should not contain more than 50 characters.")]
    [MinLength(0, ErrorMessage = "The Description field must contain at least 3 characters.")]
    public string? Description { get; set; }

    public string? DisplayName { get; set; }
    public string? DistinguishedName { get; set; }
    [Required(ErrorMessage = "The name field is required.")]
    [MaxLength(50, ErrorMessage = "The name field should not contain more than 50 characters.")]
    [MinLength(3, ErrorMessage = "The name field must contain at least 3 characters.")]
    public string? Name { get; set; }
    public string? SamAccountName { get; set; }
    public string? UserPrincipalName { get; set; }


    public void AddMember(AdObject? adObject)
    {
        if (adObject == null || _adObjects.Exists(c => c.ObjectGuid == adObject.ObjectGuid)) return;
        if (adObject is User user)
        {
            if (_adObjects.OfType<User>().Any(u => u.SamAccountName == user.SamAccountName))
            {
                return;
            }
        }
        _adObjects.Add(adObject);
    }

    public void RemoveMember(AdObject? adObject)
    {
        _adObjects.Remove(adObject);
    }

    public List<AdObject?> AllMembers()
    {
        return _adObjects;
    }

    public List<AdObject?> AllMembers(Predicate<AdObject?> predicate)
    {
        return _adObjects.FindAll(predicate);
    }

    public AdObject? GetMember(Guid guid)
    {
        if (ObjectGuid.Equals(guid))
        {
            return this;
        }

        return _adObjects.FirstOrDefault(adObject => adObject != null && adObject.ObjectGuid.Equals(guid));
    }

    public void RemoveMembers()
    {

        _adObjects.Clear();

    }
}