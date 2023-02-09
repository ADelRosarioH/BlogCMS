using BlogCMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogCMS.Infrastructure.Context.Configurations;

public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Content)
            .HasMaxLength(512);
        
        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}