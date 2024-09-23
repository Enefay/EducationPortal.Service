using EducationPortal.DataAccessLayer.Abstract;
using EducationPortal.DataAccessLayer.Concrete;
using EducationPortal.DataAccessLayer.Repositories;
using EducationPortal.EntityLayer.Entities;
using EducationPortal.EntityLayer.Enums;
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

        public Education GetByIdDetail(int id)
        {
            return _context.Educations.Select(x => new Education
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Category = x.Category == null ? null : new Category
                {
                    Id=x.Category.Id,
                    Name = x.Category.Name
                },
                Cost = x.Cost,
                Description = x.Description,
                EducationStatus = x.EducationStatus,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                Quota = x.Quota,
                Title = x.Title,
                InstructorId = x.InstructorId,
                Instructor = x.Instructor == null ? null : new AppUser
                {
                    Id = x.Instructor.Id,
                    Name = x.Instructor.Name,
                    Surname = x.Instructor.Surname
                },
                Contents = x.Contents == null ? null : x.Contents.Select(x=> new Content
                {
                    Id = x.Id,
                    FilePath = x.FilePath,
                    Type = x.Type,
                    EducationId = x.EducationId
                }).ToList(),
                EducationUsers = x.EducationUsers == null ? null : x.EducationUsers.Select(x=> new EducationUser
                {
                    Id=x.Id,
                    JoinRequestStatus = x.JoinRequestStatus,
                    LeaveRequestStatus = x.LeaveRequestStatus,
                    EducationId = x.EducationId,
                    ParticipantId = x.ParticipantId
                }).ToList(),
            }).FirstOrDefault(x => x.Id == id);
        }

        //eğer admin ise onaylanmış ve reddedilmiş talepler gelmeyecek.
        public List<Education> GetEducationsAdmin() 
        {
            return _context.Educations.Where(x => x.EducationStatus == EducationStatus.Pending).ToList();
        }

        //öğrenci ise  sadece onaylanmış ve tarihi bugunun tarihinden sonra ise ve katılmadıysa
        public List<Education> GetEducationsStudent(int studentId)
        {
            return _context.Educations
                .Where(x =>
                    x.EducationStatus == EducationStatus.Approved &&
                    x.StartDate >= DateTime.Now &&
                    (!x.EducationUsers.Any(eu => eu.ParticipantId == studentId) ||
                     x.EducationUsers.Any(eu => eu.ParticipantId == studentId && eu.JoinRequestStatus == RequestStatus.NoAction)))
                .ToList();
        }


        // öğretmen ise kendisine ait tamamlanmamış tüm eğitimleri görmeli.
        public List<Education> GetEducationsTeacher(int teacherId) 
        {
            return _context.Educations.Where(x => x.InstructorId == teacherId && x.StartDate >= DateTime.Now).ToList();
        }

        public List<Education> GetEvaluateEducationList()
        {
            return _context.Educations.Where(x => x.EducationStatus == EducationStatus.Approved).ToList();
        }

        public List<Education> GetStudentAllEducationsList(int studentId)
        {
            return _context.Educations.Where(e => e.EducationUsers.Any(eu => eu.ParticipantId == studentId))
                    .ToList();
        }

        public List<Education> GetStudentComingEducations(int studentId) //Onaylanmış, yakında baslayacak ve öğrencinin kaydının olduğu.
        {
            return _context.Educations.Where(x => x.EducationUsers.Any(eu => eu.ParticipantId == studentId && eu.JoinRequestStatus == RequestStatus.Approved ) &&
                x.EducationStatus == EducationStatus.Approved &&
                x.StartDate > DateTime.Now && x.EndDate > DateTime.Now
            ).ToList();
        }

        public List<Education> GetStudentCompletedEducations(int studentId)   //Onaylanmış, tamamlanan ve öğrencinin kaydının olduğu.
        {
            return _context.Educations.Where(x => x.EducationUsers.Any(eu => eu.ParticipantId == studentId && eu.JoinRequestStatus == RequestStatus.Approved) &&
                x.EducationStatus == EducationStatus.Approved &&
                x.EndDate < DateTime.Now
            ).ToList();
        }

        public List<Education> GetStudentContinueEducations(int studentId) //Onaylanmış, devam eden ve öğrencinin kaydının olduğu.
        {
            return _context.Educations.Where(x => x.EducationUsers.Any(eu => eu.ParticipantId == studentId && eu.JoinRequestStatus == RequestStatus.Approved) &&
               x.EducationStatus == EducationStatus.Approved &&
               x.EndDate >= DateTime.Now && x.StartDate <= DateTime.Now
           ).ToList();
        }

        public List<Education> GetTeacherAllEducationsList(int teacherId) 
        {
            return _context.Educations.Where(e => e.InstructorId == teacherId).ToList();
        }

        public List<Education> GetTeacherComingEducations(int teacherId) //Onaylanmış, yakında baslayacak ve öğrencinin kaydının olduğu.
        {
            return _context.Educations.Where(x => x.InstructorId == teacherId &&
              x.EducationStatus == EducationStatus.Approved &&
              x.StartDate > DateTime.Now && x.EndDate > DateTime.Now
          ).ToList();
        }

        public List<Education> GetTeacherCompletedEducations(int teacherId)
        {
            return _context.Educations.Where(x => x.InstructorId == teacherId &&
          x.EducationStatus == EducationStatus.Approved &&
                x.EndDate < DateTime.Now
      ).ToList();
        }

        public List<Education> GetTeacherContinueEducations(int teacherId)
        {
            return _context.Educations.Where(x => x.InstructorId == teacherId &&
          x.EducationStatus == EducationStatus.Approved &&
               x.EndDate >= DateTime.Now && x.StartDate <= DateTime.Now
      ).ToList();
        }
    }
}
