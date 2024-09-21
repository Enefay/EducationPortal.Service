using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DataAccessLayer.Abstract
{
    public interface IEducationDal : IGenericDal<Education>
    {
        List<Education> GetEvaluateEducationList();
        List<Education> GetTeacherAllEducationsList(int teacherId);
        List<Education> GetStudentAllEducationsList(int studentId);
    }
}
