using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Delete
{
    public  class DeleteStockCommandHandler:IRequestHandler<DeleteStockCommand,string>
    {
        private readonly IApplicationDbContext _context;
        public DeleteStockCommandHandler(IApplicationDbContext conext)
        {

            _context = conext;
            
        }


        public async Task<string> Handle(DeleteStockCommand request, CancellationToken ct)
        {


        
            // Find the stock by Id
            // var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == request.Id, ct);
           // var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == request.Id, ct);
          // var stock = await _context.Stocks.FindAsync(s=> s.Id == request.Id);

            var stock = await _context.Stocks.FindAsync( new object[] { request.Id }, ct);


            if (stock == null)
                return "Stock not found";

            // Remove stock
            _context.Stocks.Remove(stock);

            // Save changes (EF Core handles transaction internally)
            await _context.SaveChangesAsync(ct);

            return "Stock deleted successfully. Assignments preserved with NULL reference.";



        }

    }
}
