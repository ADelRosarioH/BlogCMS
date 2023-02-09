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
            .ForMember(src => src.CreatedBy, opt => opt.MapFrom(dest => dest.CreatedByUser.UserName))
            .ReverseMap();
        
        CreateMap<Post, OwnPostViewModel>()
            .ForMember(src => src.CreatedBy, opt => opt.MapFrom(dest => dest.CreatedByUser.UserName))
            .ReverseMap();
        
        CreateMap<Comment, PostCommentViewModel>();
        CreateMap<Feedback, PostFeedbackViewModel>()
            .ForMember(src => src.CreatedBy, opt => opt.MapFrom(dest => dest.CreatedByUser.UserName));
    }
}