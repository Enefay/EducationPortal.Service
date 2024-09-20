using EducationPortal.DtoLayer.EducationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.InstructorDto
{
    public class UpdateInstructorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInternal { get; set; }
    }
}
