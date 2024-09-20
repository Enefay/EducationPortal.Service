using EducationPortal.DtoLayer.ContentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.EducationDto
{
    public class UpdateEducationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        public int Quota { get; set; }
        public decimal Cost { get; set; }
        public int DurationInDays { get; set; }
        public ICollection<GetContentDto> Contents { get; set; }
    }
}
