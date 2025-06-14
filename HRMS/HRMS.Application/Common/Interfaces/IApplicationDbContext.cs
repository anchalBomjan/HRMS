using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Interfaces
{
    public  interface IApplicationDbContext
    {

        DbSet<Employee> Employees { get; }
        DbSet<Stock> Stocks { get; }
        DbSet<StockAssignment> StockAssignments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DatabaseFacade Database { get; } // ✅ Required for transactions
    }
}
