using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models
{
    
    public class DepartmentItem
    {
       
        public long Id { get; set; }
        public string Name { get; set; }
        // public ICollection<EmployeeItem> Employees {get; set;}

    
    }
    
}