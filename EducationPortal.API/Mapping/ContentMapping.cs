using AutoMapper;
using EducationPortal.DtoLayer.ContentDto;
using EducationPortal.EntityLayer.Entities;

namespace EducationPortal.API.Mapping
{
    public class ContentMapping:Profile
    {
        public ContentMapping()
        {
            CreateMap<Content, ResultContentDto>().ReverseMap();
            CreateMap<Content, CreateContentDto>().ReverseMap();
            CreateMap<Content, GetContentDto>().ReverseMap();
            CreateMap<Content, UpdateContentDto>().ReverseMap();
        }
    }
}
