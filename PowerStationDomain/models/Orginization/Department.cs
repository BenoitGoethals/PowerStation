namespace PowerStationDomain.models.Orginisation;

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
  //  public Employee Manager { get; set; }
    public string? Description { get; set; }
   // public List<Employee> Employees { get; set; }

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