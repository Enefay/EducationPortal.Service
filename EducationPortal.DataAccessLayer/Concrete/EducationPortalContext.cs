using EducationPortal.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.DataAccessLayer.Concrete
{
    public class EducationPortalContext : IdentityDbContext<AppUser, AppRole, int>
    {

        public EducationPortalContext(DbContextOptions<EducationPortalContext> options)
            : base(options)
        {
        }
      
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EducationParticipant> EducationParticipants { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
