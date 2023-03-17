using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PowerStationDomain.models.Orginization;

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [XmlIgnore]
    public Department? ParentDepartment { get; set; }
    public string? Description { get; set; }

    public List<Department> Departments => _departments;

    private readonly List<Department> _departments = new List<Department>();

    public bool AddDepartment(Department department)
    {
        if(_departments.Contains(department)) return false;
        _departments.Add(department);
        return true;
    }

    [OnDeserialized()]
    internal void OnSerializedMethod(StreamingContext context)
    {
        // Setting this as parent property for Child object
        ParentDepartment = this;
    }

    protected bool Equals(Department other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Department)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}