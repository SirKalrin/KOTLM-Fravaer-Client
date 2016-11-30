using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public class User : AbstractEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Absence> Absences { get; set; }
        public Department Department { get; set; }

        public enum Role
        {
            Employee, DepartmentChief, Admin
        }

    }
}
