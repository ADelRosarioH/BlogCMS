using BlogCMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogCMS.Infrastructure.Context.Configurations;

public class FeedbackEntityTypeConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Comment)
            .HasMaxLength(256);

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}