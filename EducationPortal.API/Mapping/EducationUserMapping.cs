using AutoMapper;
using EducationPortal.DtoLayer.EducationParticipantDto;
using EducationPortal.EntityLayer.Entities;

namespace EducationPortal.API.Mapping
{
    public class EducationUserMapping:Profile
    {
        public EducationUserMapping()
        {
            CreateMap<EducationUser, ResultEducationUserDto>().ReverseMap();
            CreateMap<EducationUser, CreateEducationUserDto>().ReverseMap();
            CreateMap<EducationUser, GetEducationUserDto>().ReverseMap();
            CreateMap<EducationUser, UpdateEducationUserDto>().ReverseMap();
        }
    }
}
