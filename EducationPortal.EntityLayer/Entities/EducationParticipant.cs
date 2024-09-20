using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.EntityLayer.Entities
{
    public class EducationParticipant
    {
        public int Id { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}
