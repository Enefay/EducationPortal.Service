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
        Education GetByIdDetail(int id);
        List<Education> GetEvaluateEducationList();
        List<Education> GetTeacherAllEducationsList(int teacherId);
        List<Education> GetStudentAllEducationsList(int studentId);


        //Role ve kişiye göre eğitim listesi
        List<Education> GetEducationsStudent(int studentId);
        List<Education> GetEducationsTeacher(int teacherId);
        List<Education> GetEducationsAdmin();


        //Profil için gerekli kısımlar 
        //öğrencilerin sadece kayıt olduğu! //öğretmenlerin sadece onaylanan ve kendi eğitimleri!

        //öğrenci
        List<Education> GetStudentCompletedEducations(int studentId); //Onaylanmış, tamamlanan ve öğrencinin kaydının olduğu.
        List<Education> GetStudentContinueEducations(int studentId); //Onaylanmış, devam eden ve öğrencinin kaydının olduğu.
        List<Education> GetStudentComingEducations(int studentId); //Onaylanmış, yakında baslayacak ve öğrencinin kaydının olduğu.

        //öğretmen
        List<Education> GetTeacherCompletedEducations(int teacherId); //Onaylanmış, tamamlanan ve öğretmenin kaydının olduğu.
        List<Education> GetTeacherContinueEducations(int teacherId); //Onaylanmış, devam eden ve öğretmenin kaydının olduğu.
        List<Education> GetTeacherComingEducations(int teacherId); //Onaylanmış, yakında baslayacak ve öğretmenin kaydının olduğu.
    }
}
