using EmployeeManagementApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApi.Data
{
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }

      public DbSet<Employee> Employees { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Employee>(entity =>
         {
            entity.Property(e => e.Departemen)
                   .HasConversion<string>();

            entity.Property(e => e.Jabatan)
                   .HasConversion<string>();
         });
      }
   }
}
