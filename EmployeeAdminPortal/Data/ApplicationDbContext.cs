using EmployeeAdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Office> Offices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Office) // Employee has one Office
                .WithMany() // Office has many Employees
                .HasForeignKey(e => e.OfficeId); // Foreign key

            base.OnModelCreating(modelBuilder);
        }
    }
}
