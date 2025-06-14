using HRMS.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery.ViewModel
{
    public  class StockAssignmentViewModel
    {
        public int StockId { get; set; }
        public string StockName { get; set; }
        public string Description { get; set; }
        public StockType StockType { get; set; }
        public decimal AssignedQuantity { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}
