using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.employee.GetEmployeeStockAssignmentQuery.ViewModel
{
    public  class EmployeeStockAssignmentViewModel
   
    
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public decimal AssignedQuantity { get; set; }
        public string StockName { get; set; }
        public decimal StockTotalQuantity { get; set; }
    }
}
