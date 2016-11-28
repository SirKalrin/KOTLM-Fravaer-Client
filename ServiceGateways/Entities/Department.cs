using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public List<Employee> Employees { get; set; }
        public DeptChief DepartmentChief { get; set; }

    }
}
