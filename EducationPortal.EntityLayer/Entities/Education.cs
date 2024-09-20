using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.EntityLayer.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public int Quota { get; set; }
        public decimal Cost { get; set; }
        public int DurationInDays { get; set; }
        public ICollection<Content> Contents { get; set; }
        //public ICollection<EducationParticipant> EducationParticipants { get; set; }
    }
}
