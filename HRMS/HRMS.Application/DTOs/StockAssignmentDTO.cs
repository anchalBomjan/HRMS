using HRMS.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public  class StockAssignmentDTO
    {
        public int Id { get; set; }
       
        public string EmployeeName { get; set; }
       
        public string StockName { get; set; }
        public StockType StockType { get; set; } // Critical for UI differentiation
        public DateTime? AssignmentDate { get; set; }
        public decimal AssignedQuantity { get; set; }
    }
}
