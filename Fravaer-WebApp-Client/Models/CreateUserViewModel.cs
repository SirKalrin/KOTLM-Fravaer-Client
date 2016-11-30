using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceGateways.Entities;

namespace Fravaer_WebApp_Client.Models
{
    public class CreateUserViewModel
    {
        public User User { get; set; }
        public List<Department> Departments { get; set; }
    }
}