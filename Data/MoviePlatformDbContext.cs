using MoviePlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace MoviePlatform.Data;

public class MoviePlatformDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public MoviePlatformDbContext(DbContextOptions<MoviePlatformDbContext> dbContextOptions)
        : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviePlatformDbContext).Assembly);
    }
}