using HRMS.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public  class Stock
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //use  enum for stock types
        public StockType Type { get; set; } = StockType.UnKnown;
      
        public decimal Quantity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public  ICollection<StockAssignment> Assignments { get; set; }

    }


}
