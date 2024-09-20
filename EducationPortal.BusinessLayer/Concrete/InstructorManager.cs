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
    public class InstructorManager : IInstructorService
    {
        private readonly IInstructorDal _instructorDal;

        public InstructorManager(IInstructorDal instructorDal)
        {
            _instructorDal = instructorDal;
        }

        public void TAdd(Instructor entity)
        {
            _instructorDal.Add(entity);
        }

        public void TDelete(Instructor entity)
        {
            _instructorDal.Delete(entity);
        }

        public Instructor TGetByID(int id)
        {
            return _instructorDal.GetByID(id);
        }

        public List<Instructor> TGetListAll()
        {
            return _instructorDal.GetListAll();
        }

        public void TUpdate(Instructor entity)
        {
            _instructorDal.Update(entity);
        }
    }
}
