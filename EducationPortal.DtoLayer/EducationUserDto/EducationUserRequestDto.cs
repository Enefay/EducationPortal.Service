using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DtoLayer.EducationUserDto
{
    public class EducationUserRequestDto
    {
        public List<int> RequestIds { get; set; }
        public bool Approve { get; set; }
    }
}
