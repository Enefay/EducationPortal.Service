using EducationPortal.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.EntityLayer.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string FilePath { get; set; }  // İçerik dosyasının yolu
        public ContentType Type { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
