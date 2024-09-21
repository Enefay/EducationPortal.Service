using EducationPortal.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.EntityLayer.Entities
{
    public class EducationUser
    {
        public int Id { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
        public int ParticipantId { get; set; }
        public AppUser Participant { get; set; }
        public RequestStatus JoinRequestStatus { get; set; }
        public RequestStatus LeaveRequestStatus { get; set; }
    }
}
