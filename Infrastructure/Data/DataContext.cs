using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd(); 
        
        modelBuilder.Entity<Shift>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd(); 
        
        base.OnModelCreating(modelBuilder);
    }
}