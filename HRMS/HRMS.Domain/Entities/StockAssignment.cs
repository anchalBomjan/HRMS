using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public  class StockAssignment
    {

        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int? StockId { get; set; }

        public DateTime? AssigmentDate { get; set; }= DateTime.Now;
         public decimal AsssignedQuantity { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Stock Stock { get; set; }


    }
}
