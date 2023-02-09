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
            .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
            .ReverseMap();
        
        CreateMap<Post, OwnPostViewModel>()
            .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
            .ReverseMap();

        CreateMap<Comment, PostCommentViewModel>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.UserName));

        CreateMap<Feedback, PostFeedbackViewModel>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.UserName));
    }
}