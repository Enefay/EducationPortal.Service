using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DtoLayer.UserDto;
using EducationPortal.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEducationService _educationService;

        public ProfileController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IEducationService educationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _educationService = educationService;
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId == null)
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var rolesString = string.Join(", ", roles);

           
            return Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.Surname,
                RoleName = rolesString
            });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateProfileUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.Email = updateUserDto.Email;
            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;

            // Şifre güncelleme
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    return BadRequest(removePasswordResult.Errors);
                }

                var addPasswordResult = await _userManager.AddPasswordAsync(user, updateUserDto.Password);
                if (!addPasswordResult.Succeeded)
                {
                    return BadRequest(addPasswordResult.Errors);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message ="Profil başarıyla güncellendi." });
        }



        //öğrencilerin sadece katıldıkları !!! öğretmenlerin de sadece sahibi oldugu.
        [HttpGet("GetComingEducations")]
        public async Task<IActionResult> GetComingEducations()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
           
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (role == "Teacher")
            {
                var list = _educationService.TGetTeacherComingEducations(userId);
                return Ok(list);
            }
            else
            {
                var list = _educationService.TGetStudentComingEducations(userId);
                return Ok(list);
            }
         
        }

        [HttpGet("GetContinueEducations")]
        public async Task<IActionResult> GetContinueEducations()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (role == "Teacher")
            {
                var list = _educationService.TGetTeacherContinueEducations(userId);
                return Ok(list);
            }
            else
            {
                var list = _educationService.TGetStudentContinueEducations(userId);
                return Ok(list);
            }
        }

        [HttpGet("GetCompletedEducations")]
        public async Task<IActionResult> GetCompletedEducations()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (role == "Teacher")
            {
                var list = _educationService.TGetTeacherCompletedEducations(userId);
                return Ok(list);
            }
            else
            {
                var list = _educationService.TGetStudentCompletedEducations(userId);
                return Ok(list);
            }
        }
    }
}
