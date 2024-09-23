using EducationPortal.EntityLayer.Entities;
using EducationPortal.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.EducationParticipantDto
{
    public class GetEducationUserDto
    {
        public int Id { get; set; }
        public int EducationId { get; set; }
        public int ParticipantId { get; set; }
        public RequestStatus JoinRequestStatus { get; set; }
        public RequestStatus LeaveRequestStatus { get; set; }
    }
}
