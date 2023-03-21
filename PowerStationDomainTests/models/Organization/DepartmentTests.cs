using System.Collections.Generic;
using FluentAssertions;
using PowerStationDomain.models.organisation;
using Xunit;

namespace PowerStationDomainTests.models.Organization
{
    public class DepartmentTests
    {
        [Fact()]
        public void AddDepartmentTest()
        {
            var departmentTop = new Department();

            var departments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Description = "IT",
                    Name = "IT",
                    ParentDepartment = departmentTop
                    
                },

                new Department()
                {
                    Id = 2,
                    Description = "HR",
                    Name = "HR",
                    ParentDepartment = departmentTop
                },

                new Department()
                {
                    Id = 3,
                    Description = "SALES",
                    Name = "SALES",
                    ParentDepartment = departmentTop
                },

                new Department()
                {
                    Id = 4,
                    Description = "MANAGEMENT",
                    Name = "MANAGEMENT",
                    ParentDepartment = departmentTop
                },
                new Department()
                {
                    Id = 5,
                    Description = "ADMIN",
                    Name = "ADMIN",
                    ParentDepartment = departmentTop
                },
            };
            departments.ForEach(d => departmentTop.AddDepartment(d));
            departmentTop.Departments.Should().HaveCount(5);

        }
    }
}