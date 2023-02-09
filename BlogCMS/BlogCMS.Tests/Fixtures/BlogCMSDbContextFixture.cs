using System;
using System.Linq;
using AutoFixture;
using BlogCMS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogCMS.Tests.Fixtures;

[CollectionDefinition("BlogCMS.Database")]
public class BlogCMSDbContextCollection : ICollectionFixture<BlogCMSDbContextFixture>
{
    /*
     * This class has no code, and is never created.
     * Its purpose is simply to be the place to apply [CollectionDefinition] and all the
     * ICollectionFixture<> interfaces.
     * XUnit docs: https://xunit.net/docs/shared-context#collection-fixture
     */
}

public class BlogCMSDbContextFixture : IDisposable
{
    private static readonly Fixture Fixture = new();
    public readonly BlogCMSDbContext _context;

    static BlogCMSDbContextFixture()
    {
        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    public BlogCMSDbContextFixture()
    {
        var options = new DbContextOptionsBuilder<BlogCMSDbContext>()
            .UseInMemoryDatabase(databaseName: $"BlogCMS.Database.{Guid.NewGuid()}")
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;

        _context = new BlogCMSDbContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}