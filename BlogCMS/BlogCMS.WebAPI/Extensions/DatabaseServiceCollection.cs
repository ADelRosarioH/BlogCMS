using BlogCMS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogCMS.WebAPI.Extensions;

public static class DatabaseServiceCollection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogCMSDbContext>(
            opts => opts.UseSqlServer(configuration.GetConnectionString("BlogCMS"),
                b => b.MigrationsAssembly(typeof(BlogCMSDbContext).Assembly.FullName)));
    }
}