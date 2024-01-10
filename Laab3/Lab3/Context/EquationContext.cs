using Lab3.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Context;

public class EquationContext : DbContext
{
    public DbSet<Equation> Equations { get; set; }
    
    public EquationContext(DbContextOptions<EquationContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equation>()
            .HasKey(e => e.EquationId);
    }
}
