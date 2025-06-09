using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, IdentityRole, string>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockAssignment> StockAssignments { get; set; }


        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default) =>
            base.SaveChangesAsync(cancellationToken);


        protected override void OnModelCreating(ModelBuilder builder)
        {   // Stock configuration
            builder.Entity<Stock>(entity =>
            {
                // Store StockType enum as string
                entity.Property(s => s.Type)
                    .HasConversion<string>();

                // Decimal precision for quantity
                entity.Property(s => s.Quantity)
                    .HasColumnType("decimal(18,2)");

                // Constraints
                entity.HasCheckConstraint("CK_Stock_Quantity_NonNegative", "Quantity >= 0");
                entity.HasCheckConstraint("CK_Stock_Asset_WholeNumber",
                    "Type != 'Asset' OR (Quantity = ROUND(Quantity, 0))");
            });

            // StockAssignment configuration
            builder.Entity<StockAssignment>(entity =>
            {
                // Quantity constraints
                entity.HasCheckConstraint("CK_Assignment_Quantity_Positive", "AssignedQuantity > 0");
                entity.HasCheckConstraint("CK_Assignment_Asset_SingleUnit",
                    "StockId NOT IN (SELECT Id FROM Stocks WHERE Type = 'Asset') OR AssignedQuantity = 1");
            });

           

        }
    }
}
