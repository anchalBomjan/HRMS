using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public ICollection<StockAssignment> StockAssignments { get; set; } 

    }
}
