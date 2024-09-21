using Microsoft.AspNetCore.Identity;

namespace EducationPortal.EntityLayer.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool? IsInternal { get; set; }
    }
}
