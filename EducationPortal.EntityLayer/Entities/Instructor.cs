using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.EntityLayer.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsNew { get; set; } //yeni kayit mi
        public bool IsInternal { get; set; }  // İç eğitmen mi dış eğitmen mi bilgisi
        public ICollection<Education> Educations { get; set; }
    }
}
