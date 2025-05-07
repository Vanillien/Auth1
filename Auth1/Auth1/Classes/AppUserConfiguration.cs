using Auth1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth1.Classes;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Orders");
        
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.Password)
            .HasColumnName("Password")
            .HasMaxLength(50)
            .IsRequired();
    }
}