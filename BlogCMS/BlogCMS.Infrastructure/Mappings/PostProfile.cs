using AutoMapper;
using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Models;

namespace BlogCMS.Infrastructure.Mappings;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<NewPostViewModel, Post>();
        CreateMap<UpdatePostViewModel, Post>();
        
        CreateMap<Post, PostViewModel>()
            .ReverseMap();
        
        CreateMap<Post, OwnPostViewModel>()
            .ReverseMap();
        
        CreateMap<Comment, PostCommentViewModel>();
        CreateMap<Feedback, PostFeedbackViewModel>();
    }
}