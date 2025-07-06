using HRMS.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Update
{
    public  class UpdateStockCommand:IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }

       
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StockType Type { get; set; }
        public bool IsDozen { get; set; }= false;
        // public bool IsAdditive  = true; // New: false = absolute, true = additive
        public bool IsAdditive { get; set; } = true; // ✅ CORRECT


    }
}
