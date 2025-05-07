using Auth1.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth1.Classes;

public class AuthDbContext: DbContext
{
    public DbSet<AppUser> Users { get; set; }
    
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
    }
}