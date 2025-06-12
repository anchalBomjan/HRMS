using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public  class StockAssignmentDTO
    {
        public  int Id {  get; set; }
        public string Name { get; set; }
        public string? StockName { get; set; }
        public decimal AssignedQuantity {  get; set; }
        public DateTime? AssignmentDate { get; set; }
    }
}
