using AutoMapper;
using CMSProjectServer.Domain.Dto;
using CMSProjectServer.Domain.Dto.SiteContents;
using CMSProjectServer.Domain.Entities;
using CMSProjectServer.Domain.Entities.SiteContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contents, ContentsDto>().ReverseMap();
        CreateMap<BaseComponent, BaseComponentDto>().ReverseMap();
        CreateMap<BlockComponent, BlockComponentDto>().ReverseMap();
        CreateMap<Contents, ContentsDto>().ReverseMap();
        CreateMap<Footer, FooterDto>().ReverseMap();
        CreateMap<Grid, GridDto>().ReverseMap();
        CreateMap<Header, HeaderDto>().ReverseMap();
        CreateMap<ImageComponent, ImageComponentDto>().ReverseMap();
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        CreateMap<Row, RowDto>().ReverseMap();
        CreateMap<Site, SiteDto>()
            .ForMember(dest => dest.Site, opt => opt.MapFrom(x => x.SiteContent))
            .ReverseMap()
            .ForMember(dest => dest.SiteContent, opt => opt.MapFrom(x => x.Site));
        CreateMap<Site, OldSite>().ReverseMap();
        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
            .ReverseMap();
        CreateMap<Article, ArticleShortDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(x => x.Author.Id))
            .ReverseMap();
    }
}