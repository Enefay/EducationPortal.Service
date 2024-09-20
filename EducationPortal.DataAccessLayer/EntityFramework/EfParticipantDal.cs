using EducationPortal.DataAccessLayer.Abstract;
using EducationPortal.DataAccessLayer.Concrete;
using EducationPortal.DataAccessLayer.Repositories;
using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DataAccessLayer.EntityFramework 
{
    public class EfParticipantDal : GenericRepository<Participant>, IParticipantDal
    {
        public EfParticipantDal(EducationPortalContext context) : base(context)
        {
        }
    }
}
