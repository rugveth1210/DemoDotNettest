using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models
{
    public class EmployeeItem
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DepartmentItem Department { get; set; }
        public long Salary { get; set; }

    }
}