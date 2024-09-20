using AutoMapper;
using EducationPortal.DtoLayer.EducationParticipantDto;
using EducationPortal.EntityLayer.Entities;

namespace EducationPortal.API.Mapping
{
    public class EducationParticipantMapping:Profile
    {
        public EducationParticipantMapping()
        {
            CreateMap<EducationParticipant, ResultEducationParticipantDto>().ReverseMap();
            CreateMap<EducationParticipant, CreateEducationParticipantDto>().ReverseMap();
            CreateMap<EducationParticipant, GetEducationParticipantDto>().ReverseMap();
            CreateMap<EducationParticipant, UpdateEducationParticipantDto>().ReverseMap();
        }
    }
}
