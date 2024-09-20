using AutoMapper;
using EducationPortal.DtoLayer.InstructorDto;
using EducationPortal.EntityLayer.Entities;

namespace EducationPortal.API.Mapping
{
    public class InstructorMapping:Profile
    {
        public InstructorMapping()
        {
            CreateMap<Instructor, ResultInstructorDto>().ReverseMap();
            CreateMap<Instructor, CreateInstructorDto>().ReverseMap();
            CreateMap<Instructor, GetInstructorDto>().ReverseMap();
            CreateMap<Instructor, UpdateInstructorDto>().ReverseMap();
        }
    }
}
