using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.StockSummary
{
    public class StockSummaryViewModel
    {

        //    public string StockType { get; set; }          
        //    public decimal TotalQuantity { get; set; }    
        //    public decimal UsedQuantity { get; set; }


        public int StockId { get; set; }
        public string StockName { get; set; }
        public string Description { get; set; }
        public string StockType { get; set; } // Converted enum to string
        public decimal TotalQuantity { get; set; }
        public decimal UsedQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
    }
    }
