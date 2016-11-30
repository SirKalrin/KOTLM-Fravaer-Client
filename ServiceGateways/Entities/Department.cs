using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public class Department : AbstractEntity
    {
        public string Name { get; set; }
        public List<User> Employees { get; set; }
        public User DepartmentChief { get; set; }

    }
}
