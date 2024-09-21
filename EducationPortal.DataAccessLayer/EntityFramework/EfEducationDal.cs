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
    public class EfEducationDal : GenericRepository<Education>, IEducationDal
    {
        private readonly EducationPortalContext _context;
        public EfEducationDal(EducationPortalContext context) : base(context)
        {
            _context = context;
        }

        public List<Education> GetEvaluateEducationList()
        {
            return _context.Educations.Where(x=>x.IsConfirm == null).ToList();
        }

        public List<Education> GetStudentAllEducationsList(int studentId)
        {
            return _context.Educations.Where(e => e.EducationUsers.Any(eu => eu.ParticipantId == studentId))
                    .ToList();
        }

        public List<Education> GetTeacherAllEducationsList(int teacherId)
        {
            return _context.Educations.Where(e => e.InstructorId == teacherId).ToList();
        }
    }
}
