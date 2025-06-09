using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
    }
}
