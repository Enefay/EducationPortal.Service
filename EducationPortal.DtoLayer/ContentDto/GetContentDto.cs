using EducationPortal.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.ContentDto
{
    public class GetContentDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }  
        public ContentType Type { get; set; }
        public int EducationId { get; set; }
    }
}
