using AutoMapper;
using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DtoLayer.EducationDto;
using EducationPortal.DtoLayer.UserDto;
using EducationPortal.EntityLayer.Entities;
using EducationPortal.EntityLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationsController : ControllerBase
    {
        private readonly IEducationService _educationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager; 
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EducationsController(IEducationService educationService, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IWebHostEnvironment webHostEnvironment, IContentService contentService)
        {
            _educationService = educationService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _contentService = contentService;
        }
        [HttpGet("EducationList")] //tüm eğitimler
        public async Task<IActionResult> EducationList()
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
            var rolesString = string.Join(", ", roles); //rol adı
            List<Education> educations = _educationService.TEducationsRoleAndUserList(rolesString, Convert.ToInt32(userId));
            if (educations == null)
            {
                return BadRequest(new { messages = "Bir hata oluştu." });
            }

            var value = _mapper.Map<List<ResultEducationDto>>(educations);
            return Ok(value);
        }

        [Authorize(Policy = "RequireTeacherRole")]
        [HttpPost("CreateEducation")]
        public async Task<IActionResult> CreateEducation([FromForm] CreateEducationDto createEducationDto)
        {
            // Eğitmeni bul
            AppUser teacher = await _userManager.FindByIdAsync(createEducationDto.InstructorId.ToString());

            // Eğitmenin rolünü kontrol et
            var currentRoles = await _userManager.GetRolesAsync(teacher);
            if (!currentRoles.Contains("Teacher"))
            {
                return BadRequest("Eğitimcinin rolü 'Eğitmen' olmalıdır.");
            }

            var value = _mapper.Map<Education>(createEducationDto);

            // Her içerik için dosyayı kaydet
            foreach (var content in createEducationDto.Contents)
            {
                if (content.File != null && content.File.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");
                    Directory.CreateDirectory(uploadsFolder); 

                    var uniqueFileName = content.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await content.File.CopyToAsync(fileStream);
                    }
                    content.FilePath = Path.Combine("files", uniqueFileName);

                }
            }

            _educationService.TAdd(value);

            return Ok("Eğitim başarıyla eklendi.");
        }

        [HttpPost("UpdateEducation")]
        public async Task<IActionResult> UpdateEducation([FromForm] UpdateEducationDto updateEducationDto)
        {
            // Eğitmeni bul
            AppUser teacher = await _userManager.FindByIdAsync(updateEducationDto.InstructorId.ToString());

            // Eğitmenin rolünü kontrol et
            var currentRoles = await _userManager.GetRolesAsync(teacher);
            if (!currentRoles.Contains("Teacher"))
            {
                return BadRequest("Eğitimcinin rolü 'Eğitmen' olmalıdır.");
            }

            // Mevcut Education kaydını getir
            var education =  _educationService.TGetByIdDetail(updateEducationDto.Id);
            if (education == null)
            {
                return NotFound("Eğitim bulunamadı.");
            }

           

            // Her içerik için güncelleme işlemi
            foreach (var contentDto in updateEducationDto.Contents)
            {
                var content = _contentService.TGetByID(contentDto.Id);


                if (contentDto.File != null && contentDto.File.Length > 0)
                {
                    // Dosya kaydedilecek dizin
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");
                    Directory.CreateDirectory(uploadsFolder);

                    // Eski dosya varsa sil
                    if (!string.IsNullOrEmpty(content.FilePath))
                    {
                        var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, content.FilePath);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }

                    // Yeni dosyayı kaydet
                    var uniqueFileName = contentDto.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await contentDto.File.CopyToAsync(fileStream);
                    }
                    content.FilePath = Path.Combine("files", uniqueFileName);
                }

                // Eğer yeni bir content ise ekle
                if (content == null)
                {
                    content = new Content
                    {
                        FilePath = contentDto.FilePath,
                        Type = contentDto.Type,
                        EducationId = updateEducationDto.Id
                    };
                    education.Contents.Add(content);
                }
                else
                {
                    // Mevcut content'i güncelle
                    content.Type = contentDto.Type;
                }
            }

            _educationService.TUpdate(education);

            return Ok("Eğitim başarıyla güncellendi.");
        }



        [HttpGet("GetEducationById")]
        public IActionResult GetEducationById(int id)
        {
            var value = _mapper.Map<GetEducationDto>(_educationService.TGetByIdDetail(id));
            return Ok(value);
        }

   
        
   

        //[HttpGet("GetUserEducations")] // Eğer öğrenci ise katıldığı eğitimleri görecek. Eğer öğretmen ise de içinde olduğu eğitimleri görecek. 
        //// Tüm eğitimler için geçerli 
        //public async Task<IActionResult> GetUserEducations()
        //{
        //    var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        //    if (userId == null)
        //    {
        //        return Unauthorized("Kullanıcı bilgisi alınamadı.");
        //    }
        //    var currentUser = await _userManager.FindByIdAsync(userId);
        //    if (currentUser == null)
        //    {
        //        return NotFound("Kullanıcı bulunamadı.");
        //    }
        //    var currentRoles = await _userManager.GetRolesAsync(currentUser);

        //    // Eğer kullanıcı 'Teacher' rolündeyse eğitmen olduğu eğitimleri döndür
        //    if (currentRoles.Contains("Teacher"))
        //    {
        //        // Eğitmen olduğu eğitimleri getir 
        //        var teacherEducations = _educationService.TGetTeacherAllEducationsList(currentUser.Id);

        //        var result = _mapper.Map<List<ResultEducationDto>>(teacherEducations);
        //        return Ok(result);
        //    }
        //    else if (currentRoles.Contains("Student"))
        //    {
        //        // Öğrenci olduğu eğitimleri getir 
        //        var studentEducations = _educationService.TGetStudentAllEducationsList(currentUser.Id);

        //        var result = _mapper.Map<List<ResultEducationDto>>(studentEducations);
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest("Admin rolünün katıldığı bir eğitim olamaz.");
        //    }
        //}


        /// <summary>
        /// ADMIN ADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMIN
        /// </summary>
        [HttpPut("EvaluateEducation")] //onaylama ve reddetme
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult EvaluateEducation(EvaluateEducationDto evaluateEducationDto)
        {
            Education education = _educationService.TGetByID(evaluateEducationDto.Id);
            education.EducationStatus = evaluateEducationDto.Answer == true ? EducationStatus.Approved : EducationStatus.Rejected;
            _educationService.TUpdate(education);
            return Ok("Eğitim" + (evaluateEducationDto.Answer == true ? " onaylandı." : " reddedildi."));
        }
      
        [HttpDelete("DeleteEducation")] //TODO SILINECEK VERİLER VAR !
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult DeleteEducation(int id)
        {
            var value = _educationService.TGetByID(id);
            _educationService.TDelete(value);
            return Ok("Eğitim Silindi");
        }

       
    }
}
