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
    public class EducationParticipantManager : IEducationParticipantService
    {
        private readonly IEducationParticipantDal _educationParticipantDal;

        public EducationParticipantManager(IEducationParticipantDal educationParticipantDal)
        {
            _educationParticipantDal = educationParticipantDal;
        }

        public void TAdd(EducationParticipant entity)
        {
            _educationParticipantDal.Add(entity);
        }

        public void TDelete(EducationParticipant entity)
        {
            _educationParticipantDal.Delete(entity);
        }

        public EducationParticipant TGetByID(int id)
        {
            return _educationParticipantDal.GetByID(id);
        }

        public List<EducationParticipant> TGetListAll()
        {
            return _educationParticipantDal.GetListAll();
        }

        public void TUpdate(EducationParticipant entity)
        {
            _educationParticipantDal.Update(entity);
        }
    }
}
