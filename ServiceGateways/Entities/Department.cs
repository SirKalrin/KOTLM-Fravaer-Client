﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateways.Entities
{
    public class Department : AbstractEntity
    {
        public List<Employee> Employees { get; set; }
        public DeptChief DepartmentChief { get; set; }

    }
}
