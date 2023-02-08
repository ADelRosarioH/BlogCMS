using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogCMS.Infrastructure.Context;

public class BlogCMSDbContextFactory : IDesignTimeDbContextFactory<BlogCMSDbContext>
{
    public BlogCMSDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
            .Build();

        var connectionString = config.GetConnectionString("BlogCMS");

        var options = new DbContextOptionsBuilder<BlogCMSDbContext>()
            .UseSqlServer(connectionString).Options;

        var context = new BlogCMSDbContext(options);

        return context;
    }
}