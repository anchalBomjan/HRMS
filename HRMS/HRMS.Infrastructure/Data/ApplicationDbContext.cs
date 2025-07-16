using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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



        // This exposes Database via IApplicationDbContext
     //   public DatabaseFacade Database => base.Database;
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Required for Identity configuration

            // *************Employee ***************************
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(256);

                entity.Property(e => e.Phonenumber)
                      .HasMaxLength(30);

                // Configure relationship with StockAssignment
                entity.HasMany(e => e.StockAssignments)
                      .WithOne(a => a.Employee)
                      .HasForeignKey(a => a.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ************* Stock ****************
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stocks");
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(120);

                entity.Property(s => s.Description)
                      .HasMaxLength(500);

                //entity.Property(s => s.Type)
                //      .HasConversion<string>() // Store enum as string
                //      .HasMaxLength(20);
                entity.Property(s => s.Type);


                entity.Property(s => s.Quantity)
                      .HasColumnType("decimal(18,2)")  // Supports fractional units
                      .HasDefaultValue(0);
                entity.Property(p => p.RowVersion)
                  .IsRowVersion()
                  .IsConcurrencyToken();         // critical for conflict deletion
            });

            // ****** StockAssignment*****
            modelBuilder.Entity<StockAssignment>(entity =>
            {
                entity.ToTable("StockAssignments");
                entity.HasKey(a => a.Id);

                // Configure properties with correct names
                entity.Property(a => a.AssigmentDate)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(a => a.AsssignedQuantity)
                      .HasColumnType("decimal(18,2)");

                // Configure nullable FK with SetNull behavior
                entity.Property(a => a.StockId)
                      .IsRequired(false);  // Make FK nullable

                // Configure relationship with Stock
                entity.HasOne(a => a.Stock)
                      .WithMany(s => s.Assignments)
                      .HasForeignKey(a => a.StockId)
                      .OnDelete(DeleteBehavior.SetNull);  // Critical for safe deletion

                // Configure indexes
               // entity.HasIndex(a => a.EmployeeId);
                //entity.HasIndex(a => new { a.StockId, a.EmployeeId });  // Composite index
            });

        }
    }
}
