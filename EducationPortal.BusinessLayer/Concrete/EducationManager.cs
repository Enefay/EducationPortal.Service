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

        public void TAdd(Education entity)
        {
            _educationDal.Add(entity);
        }

        public void TDelete(Education entity)
        {
            _educationDal.Delete(entity);
        }

        public Education TGetByID(int id)
        {
            return _educationDal.GetByID(id);
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
    }
}
