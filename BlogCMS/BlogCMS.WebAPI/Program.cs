using BlogCMS.Infrastructure.Context;
using BlogCMS.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddControllers();

// Add BlogCMS Services
var config = builder.Configuration;

builder.Services.AddSwagger();
builder.Services.AddDbContext(config);
builder.Services.AddIdentity();
builder.Services.AddJwt(config);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddBlogCMSServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<BlogCMSDbContext>();
    
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();