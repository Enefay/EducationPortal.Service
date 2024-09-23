using EducationPortal.EntityLayer.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.ContentDto
{
    public class CreateContentDto
    {
        public IFormFile File { get; set; } 
        public string FilePath { get; set; }  
        public ContentType Type { get; set; }
        public int? EducationId { get; set; }
    }
}
