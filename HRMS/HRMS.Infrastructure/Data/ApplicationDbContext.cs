using HRMS.Domain.Entities;
using HRMS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Data
{
    //public  class ApplicationDbContext:IdentityDbContext<ApplicationUser,IdentityRole,string> 
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole, string>,IApplicationDbContext
         

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockAssignment> StockAssignments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
