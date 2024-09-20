using AutoMapper;
using EducationPortal.DtoLayer.EducationDto;
using EducationPortal.EntityLayer.Entities;

namespace EducationPortal.API.Mapping
{
    public class EducationMapping:Profile
    {
        public EducationMapping()
        {
            CreateMap<Education, ResultEducationDto>().ReverseMap();
            CreateMap<Education, CreateEducationDto>().ReverseMap();
            CreateMap<Education, GetEducationDto>().ReverseMap();
            CreateMap<Education, UpdateEducationDto>().ReverseMap();
        }
    }
}
