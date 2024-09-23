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
    public class EducationUserManager : IEducationUserService
    {
        private readonly IEducationUserDal _educationUserDal;

        public EducationUserManager(IEducationUserDal educationUserDal)
        {
            _educationUserDal = educationUserDal;
        }

        public void TAdd(EducationUser entity)
        {
            _educationUserDal.Add(entity);
        }

        public void TDelete(EducationUser entity)
        {
            _educationUserDal.Delete(entity);
        }

        public EducationUser TGetByID(int id)
        {
            return _educationUserDal.GetByID(id);
        }

        public List<EducationUser> TGetListAll()
        {
            return _educationUserDal.GetListAll();
        }

        public EducationUser TIsSaveEducation(int educationId, int participantId)
        {
            return _educationUserDal.IsSaveEducation(educationId, participantId);
        }

        public void TUpdate(EducationUser entity)
        {
            _educationUserDal.Update(entity);
        }
    }
}
