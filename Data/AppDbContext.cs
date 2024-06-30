using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<TodoModel> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(@"Host=127.0.0.1;Username=postgres;Password=postgres;Database=baltaio");
    }
}
