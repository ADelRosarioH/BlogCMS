using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Helpers.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogCMS.Infrastructure.Context.Configurations;

public class IdentityRoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.ToTable("Roles");

        builder.HasData(new List<IdentityRole<Guid>>
        {
            new()
            {
                Id = Guid.Parse("30D123B9-36CE-409D-81DD-A90EB1C2B0E7"),
                Name = Roles.Public,
                NormalizedName = Roles.Public.Normalize().ToUpper(),
            },
            new()
            {
                Id = Guid.Parse("20E35B48-3E97-4573-BB07-9EC649CE3A6E"),
                Name = Roles.Writer,
                NormalizedName = Roles.Writer.Normalize().ToUpper(),
                
            },
            new()
            {
                Id = Guid.Parse("805BD39F-8F96-4280-922A-F43E334CCBA5"),
                Name = Roles.Editor,
                NormalizedName = Roles.Editor.Normalize().ToUpper(),
            }
        });
    }
}