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
        public List<User> Users { get; set; }

        public Department()
        {
            Users = new List<User>();
        }
    }
}
