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
    public class ParticipantManager : IParticipantService
    {
        private readonly IParticipantDal _participantDal;

        public ParticipantManager(IParticipantDal participantDal)
        {
            _participantDal = participantDal;
        }

        public void TAdd(Participant entity)
        {
            _participantDal.Add(entity);
        }

        public void TDelete(Participant entity)
        {
            _participantDal.Delete(entity);
        }

        public Participant TGetByID(int id)
        {
            return _participantDal.GetByID(id);
        }

        public List<Participant> TGetListAll()
        {
            return _participantDal.GetListAll();
        }

        public void TUpdate(Participant entity)
        {
            _participantDal.Update(entity);
        }
    }
}
