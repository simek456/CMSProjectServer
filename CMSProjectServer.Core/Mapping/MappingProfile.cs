using AutoMapper;
using CMSProjectServer.Domain.Dto;
using CMSProjectServer.Domain.Entities;

namespace CMSProjectServer.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Site, OldSite>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SiteContent, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.SiteContent, opt => opt.Ignore());
        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
            .ReverseMap();
        CreateMap<Article, ArticleShortDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(x => x.Author.Id))
            .ReverseMap();
    }
}