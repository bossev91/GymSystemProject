using System.Collections.Generic;

namespace GymSys.Data.Models
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}