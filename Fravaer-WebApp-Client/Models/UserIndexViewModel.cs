using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceGateways.Entities;

namespace Fravaer_WebApp_Client.Models
{
    public class UserIndexViewModel
    {
        public List<Department> Departments { get; set; }
        public DateTime MonthDateTime { get; set; }
        public decimal AverageDaysInt { get; set; }

    }
}