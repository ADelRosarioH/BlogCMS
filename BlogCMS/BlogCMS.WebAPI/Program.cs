using BlogCMS.Infrastructure.Context;
using BlogCMS.Infrastructure.Mappings;
using BlogCMS.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

const string CorsDevelopmentPolicyName = nameof(CorsDevelopmentPolicyName);

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(PostProfile));
});
builder.Services.AddHealthChecks();
builder.Services.AddCors(cfg =>
{
    cfg.AddPolicy(CorsDevelopmentPolicyName, builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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
    app.UseCors(CorsDevelopmentPolicyName);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health")
    .WithMetadata(new AllowAnonymousAttribute());

app.Run();