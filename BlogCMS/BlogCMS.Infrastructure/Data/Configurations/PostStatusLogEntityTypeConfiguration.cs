using BlogCMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogCMS.Infrastructure.Context.Configurations;

public class PostStatusLogEntityTypeConfiguration : IEntityTypeConfiguration<PostStatusLog>
{
    public void Configure(EntityTypeBuilder<PostStatusLog> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Post)
            .WithMany(p => p.StatusLogs)
            .HasForeignKey(p => p.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}