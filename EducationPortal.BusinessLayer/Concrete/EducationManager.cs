using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DataAccessLayer.Abstract;
using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BusinessLayer.Concrete
{
    public class EducationManager : IEducationService
    {
        private readonly IEducationDal _educationDal;

        public EducationManager(IEducationDal educationDal)
        {
            _educationDal = educationDal;
        }

        public List<Education> TGetStudentComingEducations(int studentId)
        {
            return _educationDal.GetStudentComingEducations(studentId);
        }

        public List<Education> TGetStudentCompletedEducations(int studentId)
        {
            return _educationDal.GetStudentCompletedEducations(studentId);
        }

        public List<Education> TGetStudentContinueEducations(int studentId)
        {
            return _educationDal.GetStudentContinueEducations(studentId);
        }

        public void TAdd(Education entity)
        {
            _educationDal.Add(entity);
        }

        public void TDelete(Education entity)
        {
            _educationDal.Delete(entity);
        }

        public List<Education> TEducationsRoleAndUserList(string role, int id)
        {
            if (role == "Admin")
            {
                return _educationDal.GetEducationsAdmin();
            }
            else if (role == "Student")
            {
                return _educationDal.GetEducationsStudent(id);
            }
            else if (role == "Teacher")
            {
                return _educationDal.GetEducationsTeacher(id);
            }
            else
            {
                return null;
            }
        }

        public Education TGetByID(int id)
        {
            return _educationDal.GetByID(id);
        }

        public Education TGetByIdDetail(int id)
        {
            return _educationDal.GetByIdDetail(id);
        }

        public List<Education> TGetEvaluateEducationList()
        {
           return _educationDal.GetEvaluateEducationList();
        }

        public List<Education> TGetListAll()
        {
           return _educationDal.GetListAll();
        }

        public List<Education> TGetStudentAllEducationsList(int studentId)
        {
            return _educationDal.GetStudentAllEducationsList(studentId);
        }

        public List<Education> TGetTeacherAllEducationsList(int teacherId)
        {
            return _educationDal.GetTeacherAllEducationsList(teacherId);
        }

        public void TUpdate(Education entity)
        {
            _educationDal.Update(entity);
        }

        public List<Education> TGetTeacherCompletedEducations(int teacherId)
        {
            return _educationDal.GetTeacherCompletedEducations(teacherId);
        }

        public List<Education> TGetTeacherContinueEducations(int teacherId)
        {
            return _educationDal.GetTeacherContinueEducations(teacherId);
        }

        public List<Education> TGetTeacherComingEducations(int teacherId)
        {
            return _educationDal.GetTeacherComingEducations(teacherId);
        }
    }
}
