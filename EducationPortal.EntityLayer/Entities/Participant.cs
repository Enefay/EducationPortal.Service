using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.EntityLayer.Entities
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Education> EnrolledEducations { get; set; }
    }
}
