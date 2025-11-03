using AutoMapper;
using MyApp.Comments.Core.Domain;

namespace MyApp.Comments.Core.Application;

public sealed class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.TextHtml))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<CommentDto, Comment>()
            .ForMember(dest => dest.TextHtml, opt => opt.MapFrom(src => src.Html))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TargetType, opt => opt.MapFrom(src => src.TargetType))
            .ForMember(dest => dest.TargetId, opt => opt.MapFrom(src => src.TargetId))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.RootId, opt => opt.MapFrom(src => src.RootId))
            .ForMember(dest => dest.Depth, opt => opt.MapFrom(src => src.Depth))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedByName, opt => opt.Ignore())
            .ForMember(dest => dest.HomePage, opt => opt.Ignore());
    }
}
