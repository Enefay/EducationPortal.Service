using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BusinessLayer.Abstract
{
    public interface IEducationService : IGenericService<Education>
    {
        List<Education> TGetEvaluateEducationList();
        List<Education> TGetTeacherAllEducationsList(int teacherId);
        List<Education> TGetStudentAllEducationsList(int studentId);

    }
}
