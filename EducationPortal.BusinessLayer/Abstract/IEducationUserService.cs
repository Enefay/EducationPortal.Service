using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BusinessLayer.Abstract
{
    public interface IEducationUserService : IGenericService<EducationUser>
    {
        EducationUser TIsSaveEducation(int educationId, int participantId);

    }
}
