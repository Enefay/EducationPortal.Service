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
        Education TGetByIdDetail(int id);

        List<Education> TEducationsRoleAndUserList(string role,int id);
        List<Education> TGetEvaluateEducationList();
        List<Education> TGetTeacherAllEducationsList(int teacherId);
        List<Education> TGetStudentAllEducationsList(int studentId);


        //Profil

        //öğrenci
        List<Education> TGetStudentCompletedEducations(int studentId); //Onaylanmış, tamamlanan ve öğrencinin kaydının olduğu.
        List<Education> TGetStudentContinueEducations(int studentId); //Onaylanmış, devam eden ve öğrencinin kaydının olduğu.
        List<Education> TGetStudentComingEducations(int studentId); //Onaylanmış, yakında baslayacak ve öğrencinin kaydının olduğu.


        //öğretmen
        List<Education> TGetTeacherCompletedEducations(int teacherId); //Onaylanmış, tamamlanan ve öğretmenin kaydının olduğu.
        List<Education> TGetTeacherContinueEducations(int teacherId); //Onaylanmış, devam eden ve öğretmenin kaydının olduğu.
        List<Education> TGetTeacherComingEducations(int teacherId); //Onaylanmış, yakında baslayacak ve öğretmenin kaydının olduğu.
    }
}
