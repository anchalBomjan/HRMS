﻿using HRMS.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Create
{
    public  class CreateStockCommand:IRequest<int>
    {

        public string Name { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("stockType")]

        ///Maps the C# property Type to/from the JSON property "stockType" during serialization/deserialization



        public StockType Type { get; set; }
        public decimal Quantity { get; set; }
        public bool IsDozen { get; set; }
    }
}
