using EducationPortal.DtoLayer.CategoryDto;
using EducationPortal.DtoLayer.EducationDto;
using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.InstructorDto
{
    public class CreateInstructorDto
    {
        public string Name { get; set; }
        public bool IsInternal { get; set; }  
        public ICollection<ResultEducationDto> Educations { get; set; }
    }
}
