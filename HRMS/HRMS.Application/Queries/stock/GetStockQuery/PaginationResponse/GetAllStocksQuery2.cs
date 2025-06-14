using HRMS.Application.Common.Pagination;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockQuery.PaginationResponse
{
    public class GetAllStocksQuery2: IRequest<PaginatedResponse<StockResponse>>
    {
        public int Page = 1;
        public int PageSize = 20;

        public GetAllStocksQuery2(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
