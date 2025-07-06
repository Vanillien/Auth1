using Microsoft.EntityFrameworkCore;
using MinimalApiTemplate.Models;
using MinimalApiTemplate.EF;

namespace MinimalApiTemplate.EF;

public class PostDbContext : DbContext
{
    public DbSet<Post> Posts { get; init; }

    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());
    }
}