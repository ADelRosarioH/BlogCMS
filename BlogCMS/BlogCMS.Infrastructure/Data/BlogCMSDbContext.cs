using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCMS.Infrastructure.Context;

public class BlogCMSDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public BlogCMSDbContext(DbContextOptions<BlogCMSDbContext> options)
        : base(options)        
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<IdentityUser<Guid>>().ToTable("Users");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
    }
}