using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using BlogCMS.Infrastructure.Context;
using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Mappings;
using BlogCMS.Infrastructure.Services;
using BlogCMS.Tests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace BlogCMS.Tests;

[Collection("BlogCMS.Database")]
public class PostServiceTests
{
    private readonly BlogCMSDbContext _context;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly IMapper _mapper;
    
    private readonly IPostService _sut;
    
    public PostServiceTests(BlogCMSDbContextFixture fixture)
    {
        _context = fixture._context;
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        
        var mapperConfig = new MapperConfiguration(cfg => 
            cfg.AddMaps(typeof(PostProfile)));

        _mapper = mapperConfig.CreateMapper();
        
        _sut = new PostService(_context, _mapper, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task GetApprovedPosts_ShouldReturnApprovedPost()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(s => s.CurrentUserId)
            .Returns(userId);

        var fixture = new Fixture();

        var user = fixture.Build<BlogUser>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.Posts)
            .With(p => p.Id, userId)
            .Create();
        
        var post = fixture.Build<Post>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.StatusLogs)
            .Without(p => p.UpdatedAt)
            .Without(p => p.CreatedByUser)
            .With(p => p.CreatedByUserId, userId)
            .With(p => p.Status, PostStatus.Approved)
            .Create();

        await _context.Users.AddAsync(user);
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        
        // Act
        var results = await _sut.GetApprovedPosts();

        // Assert
        results.Should().HaveCount(1);
    }

    [Fact]
    public async Task IsPostAuthor_ShouldReturnTrue_WhenUserIsPostOwner()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(s => s.CurrentUserId)
            .Returns(userId);

        var fixture = new Fixture();

        var user = fixture.Build<BlogUser>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.Posts)
            .With(p => p.Id, userId)
            .Create();
        
        var post = fixture.Build<Post>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.StatusLogs)
            .Without(p => p.UpdatedAt)
            .Without(p => p.CreatedByUser)
            .With(p => p.CreatedByUserId, userId)
            .With(p => p.Status, PostStatus.Approved)
            .Create();

        await _context.Users.AddAsync(user);
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _sut.IsPostAuthor(userId, post.Id);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task IsPostAuthor_ShouldReturnFalse_WhenUserIsNotPostOwner()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        
        _currentUserServiceMock.Setup(s => s.CurrentUserId)
            .Returns(otherUserId);

        var fixture = new Fixture();

        var user = fixture.Build<BlogUser>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.Posts)
            .With(p => p.Id, userId)
            .Create();
        
        var otherUser = fixture.Build<BlogUser>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.Posts)
            .With(p => p.Id, otherUserId)
            .Create();
        
        var post = fixture.Build<Post>()
            .Without(p => p.Comments)
            .Without(p => p.Feedbacks)
            .Without(p => p.StatusLogs)
            .Without(p => p.UpdatedAt)
            .Without(p => p.CreatedByUser)
            .With(p => p.CreatedByUserId, otherUserId)
            .With(p => p.Status, PostStatus.Approved)
            .Create();

        await _context.Users.AddAsync(user);
        await _context.Users.AddAsync(otherUser);
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _sut.IsPostAuthor(userId, post.Id);

        // Assert
        result.Should().BeFalse();
    }
}