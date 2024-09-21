using AutoMapper;
using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DtoLayer.EducationDto;
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
    [Authorize]
    public class EducationsController : ControllerBase
    {
        private readonly IEducationService _educationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager; 
        private readonly IMapper _mapper;
        public EducationsController(IEducationService educationService, IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _educationService = educationService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet("EducationList")] //tüm eğitimler
        public IActionResult EducationList()
        {
            var value = _mapper.Map<List<ResultEducationDto>>(_educationService.TGetListAll());
            return Ok(value);
        }

        [Authorize(Policy = "RequireTeacherRole")]
        [HttpPost("CreateEducation")] //todo sadece öğretmen olusturacak
        public async Task<IActionResult> CreateEducation(CreateEducationDto createEducationDto)
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
            _educationService.TAdd(value);

            return Ok("Eğitim başarıyla eklendi.");
        }

     

        [HttpGet("GetEducationById")]
        public IActionResult GetEducationById(int id)
        {
            var value = _mapper.Map<GetEducationDto>(_educationService.TGetByID(id));
            return Ok(value);
        }

        [HttpPut("UpdateEducation")]
        public IActionResult UpdateEducation(UpdateEducationDto updateEducationDto)
        {
            var value = _mapper.Map<Education>(updateEducationDto);
            _educationService.TUpdate(value);
            return Ok("Eğitim Güncellendi");
        }

        
   

        [HttpGet("GetUserEducations")] // Eğer öğrenci ise katıldığı eğitimleri görecek. Eğer öğretmen ise de içinde olduğu eğitimleri görecek. 
        // Tüm eğitimler için geçerli 
        public async Task<IActionResult> GetUserEducations()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId == null)
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            var currentRoles = await _userManager.GetRolesAsync(currentUser);

            // Eğer kullanıcı 'Teacher' rolündeyse eğitmen olduğu eğitimleri döndür
            if (currentRoles.Contains("Teacher"))
            {
                // Eğitmen olduğu eğitimleri getir 
                var teacherEducations = _educationService.TGetTeacherAllEducationsList(currentUser.Id);

                var result = _mapper.Map<List<ResultEducationDto>>(teacherEducations);
                return Ok(result);
            }
            else if (currentRoles.Contains("Student"))
            {
                // Öğrenci olduğu eğitimleri getir 
                var studentEducations = _educationService.TGetStudentAllEducationsList(currentUser.Id);

                var result = _mapper.Map<List<ResultEducationDto>>(studentEducations);
                return Ok(result);
            }
            else
            {
                return BadRequest("Admin rolünün katıldığı bir eğitim olamaz.");
            }
        }


        /// <summary>
        /// ADMIN ADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMINADMIN
        /// </summary>
        [HttpPut("EvaluateEducation")] 
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult EvaluateEducation(EvaluateEducationDto evaluateEducationDto)
        {
            Education education = _educationService.TGetByID(evaluateEducationDto.Id);
            education.IsConfirm = evaluateEducationDto.Answer;
            _educationService.TUpdate(education);
            return Ok("Eğitim" + (evaluateEducationDto.Answer == true ? " onaylandı." : " reddedildi."));
        }
        [HttpGet("EvaluateEducationList")] 
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult EvaluateEducationList()
        {
            List<Education> education = _educationService.TGetEvaluateEducationList();
            return Ok(education);
        }
        [HttpDelete("DeleteEducation")] //TODO SILINECEK VERİLER VAR !
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult DeleteEducation(int id)
        {
            var value = _educationService.TGetByID(id);
            _educationService.TDelete(value);
            return Ok("Eğitim Silindi");
        }

        /// <summary>
        /// STUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENTSTUDENT
        /// Eğitime katılma talebi, eğitimden ayrılma talebi, katılmadığım ve aktif eğitimler. katıldığım ve devam eden eğitimler.
        /// </summary>
        /// 

        [HttpGet("GetStudentNotAttendActiveEducations")] //aktif katılınmamış eğitimler.  devam edenler.
        [Authorize(Policy = "RequireStudentRole")]
        public IActionResult GetStudentNotAttendActiveEducations(int studentId)
        {
            return Ok("Eğitim Silindi");
        }
     
        [HttpGet("GetStudentContinuingEducations")] //aktif katılınmış eğitimler devam edenler.
        public IActionResult GetStudentContinuingEducations(int studentId)
        {
            return Ok("Eğitim Silindi");
        }

        [HttpGet("GetStudentCompletedEducations")] //Katılınmış ve tammamlanmış eğitimler.
        [Authorize(Policy = "RequireStudentRole")]
        public IActionResult GetStudentCompletedEducations(int studentId)
        {
            return Ok("Eğitim Silindi");
        }
        [HttpGet("GetActiveEducationDetail")] //katılınmamış ve başlamamış eğitim detayı
        [Authorize(Policy = "RequireStudentRole")]
        public IActionResult GetActiveEducationDetail(int educationId)
        {
            return Ok("Eğitim Silindi");
        }
    }
}
