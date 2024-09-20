using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EducationPortal.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DataAccessLayer.Concrete
{
    public class EducationPortalContext : IdentityDbContext<AppUser , AppRole, int>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=S117;DataBase=EducationPortalDb; uid=sa; pwd=Batuhan8047;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        //}

        public EducationPortalContext(DbContextOptions<EducationPortalContext> options)
      : base(options) // DbContext sınıfına yapılandırmayı geçiriyoruz
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
