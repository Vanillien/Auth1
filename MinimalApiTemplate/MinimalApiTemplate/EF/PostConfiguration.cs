using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalApiTemplate.Models;

namespace MinimalApiTemplate.EF;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(p => p.TimeOfCreation)
            .HasColumnName("TimeOfCreation")
            .IsRequired();

        builder.Property(p => p.TextLayout)
            .HasColumnName("TextLayout")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.AuthorUsername)
            .HasColumnName("AuthorUsername")
            .HasMaxLength(50)
            .IsRequired();
    }
}