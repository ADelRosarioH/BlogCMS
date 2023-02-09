using System.Reflection;
using BlogCMS.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCMS.Infrastructure.Context;

public class BlogCMSDbContext : IdentityDbContext<BlogUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostStatusLog> StatusLogs { get; set; }

    public BlogCMSDbContext(DbContextOptions<BlogCMSDbContext> options)
        : base(options)        
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var assembly = Assembly.GetExecutingAssembly();
        builder.ApplyConfigurationsFromAssembly(assembly);
    }
}