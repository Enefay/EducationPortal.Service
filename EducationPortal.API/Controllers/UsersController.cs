using EducationPortal.DtoLayer.UserDto;
using EducationPortal.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    // Yalnızca adminlerin erişimi 
    // Admin ve öğretmen oluşturma alanı
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {

            var user = new AppUser
            {
                UserName = createUserDto.Email,
                Email = createUserDto.Email,
                Name = createUserDto.Name,
                Surname = createUserDto.Surname
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!string.IsNullOrEmpty(createUserDto.RoleName))
            {
                await _userManager.AddToRoleAsync(user, createUserDto.RoleName);
            }

            return Ok(new { message = createUserDto.RoleName + " başarıyla oluşturuldu." });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.Email = updateUserDto.Email;
            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;


            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Rol güncelleme
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!string.IsNullOrEmpty(updateUserDto.RoleName))
            {
                await _userManager.AddToRoleAsync(user, updateUserDto.RoleName);
            }

            return Ok(new { message = updateUserDto.RoleName + " başarıyla güncellendi." });

        }


        [HttpDelete("delete/{id}")] //todo ilgili eğitimcinin bağlı olduğu eğitimler olabilir. Hata mesajı gösterilmeli.
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Kullanıcı başarıyla silindi" });
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var rolesString = string.Join(", ", roles);
            if (rolesString == "Student")
            {
                return StatusCode(403, "Sadece personellere erişebilirsiniz.");
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.Surname,
                RoleName = rolesString
            });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUsers()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }
            var users = _userManager.Users.Where(x=>x.Id.ToString() != userId);
            var userList = new List<object>();

           
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var rolesString = string.Join(", ", roles); 

                if (rolesString != "Student")
                {
                    userList.Add(new
                    {
                        user.Id,
                        user.Email,
                        user.Name,
                        user.Surname,
                        Roles = rolesString
                    });
                }
             
            }

            return Ok(userList);
        }


    }
}
