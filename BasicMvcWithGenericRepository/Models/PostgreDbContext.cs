using Microsoft.EntityFrameworkCore;

namespace BasicMvcWithGenericRepository.Models;

public class PostgreDbContext : DbContext
{
    public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();

}
