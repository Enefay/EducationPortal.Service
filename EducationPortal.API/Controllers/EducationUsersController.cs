using AutoMapper;
using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DataAccessLayer.Concrete;
using EducationPortal.DtoLayer.CategoryDto;
using EducationPortal.DtoLayer.EducationDto;
using EducationPortal.DtoLayer.EducationUserDto;
using EducationPortal.EntityLayer.Entities;
using EducationPortal.EntityLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationUsersController : ControllerBase
    {
        private IEducationUserService _educationUserService;
        private IEducationService _educationService;
        private readonly IMapper _mapper;
        private readonly EducationPortalContext _context;
        public EducationUsersController(IEducationUserService educationUserService, IMapper mapper, IEducationService educationService, EducationPortalContext context)
        {
            _educationUserService = educationUserService;
            _mapper = mapper;
            _educationService = educationService;
            _context = context;
        }


        [HttpGet("Durum")]
        public IActionResult Durum()
        {
            //Katılma talebi
            //katılma talebini ilk defa olusturmus olabilir
            //katılma talebi beklemede olmus olabilir.
            //zaten katılmıs olabilir.
            //!! katıldığı an çıkma talebi NoAction olmalı.


            //çıkma talebi

            return Ok();
        }

        [HttpGet("GetEducationUserById")]
        public IActionResult GetEducationUserById(int educationId)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            if (userId == null)
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }

            var asd = _educationUserService.TIsSaveEducation(educationId, userId);

            return Ok(asd);
        }

        [HttpPost("JoinEducation/{educationId}")]

        public IActionResult JoinEducation(int educationId)
        {
            //var education = _educationService.TGetByIdDetail(educationId);
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            if (userId == null)
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }
            // Kullanıcı zaten eğitime katılmış mı kontrol et
            var educationUser = _educationUserService.TIsSaveEducation(educationId, userId);
            if (educationUser != null && educationUser.JoinRequestStatus == RequestStatus.Approved)
            {
                return BadRequest("Eğitime zaten katıldınız.");
            }
            else if (educationUser != null && educationUser.JoinRequestStatus == RequestStatus.Pending)
            {
                return BadRequest("Katılma talebiniz zaten onay bekliyor.");
            }
            else if (educationUser != null && educationUser.JoinRequestStatus == RequestStatus.NoAction)
            {
                educationUser.JoinRequestStatus = RequestStatus.Pending;
                educationUser.LeaveRequestStatus = RequestStatus.NoAction;
                _educationUserService.TUpdate(educationUser);
                return Ok("Eğitime tekrar katılma talebiniz başarıyla alındı");
            }

            // Yeni katılım talebi oluştur
            var newEducationUser = new EducationUser
            {
                EducationId = educationId,
                ParticipantId = userId,
                JoinRequestStatus = RequestStatus.Pending
            };


            //_context.EducationUsers.Add(newEducationUser);
            _educationUserService.TAdd(newEducationUser);
            return Ok("Katılma talebi başarıyla gönderildi.");
        }

        [HttpPost("LeaveEducation/{educationId}")]
        public IActionResult LeaveEducation(int educationId)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            if (userId == null)
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }
            var educationUser = _educationUserService.TIsSaveEducation(educationId, userId);

            if (educationUser == null || educationUser.JoinRequestStatus != RequestStatus.Approved)
            {
                return BadRequest("Henüz eğitime katılmadınız veya katılma talebiniz onaylanmadı.");
            }

            if (educationUser.LeaveRequestStatus == RequestStatus.Pending)
            {
                return BadRequest("Ayrılma talebiniz zaten onay bekliyor.");
            }

            educationUser.LeaveRequestStatus = RequestStatus.Pending;
            educationUser.JoinRequestStatus = RequestStatus.NoAction;
            _educationUserService.TUpdate(educationUser);
            return Ok("Ayrılma talebi başarıyla gönderildi.");
        }
        [HttpGet("GetPendingRequests")]
        public IActionResult GetPendingRequests()
        {
            var requests = _context.EducationUsers
                .Where(eu => eu.JoinRequestStatus == RequestStatus.Pending || eu.LeaveRequestStatus == RequestStatus.Pending)
                .Select(eu => new
                {
                    eu.Id,
                    EducationTitle = eu.Education.Title,
                    ParticipantName = eu.Participant.Name,
                    JoinRequestStatus = eu.JoinRequestStatus,
                    LeaveRequestStatus = eu.LeaveRequestStatus
                }).ToList();

            return Ok(requests);
        }
        [HttpPut("EvaluateRequests")]
        public IActionResult EvaluateRequests(EducationUserRequestDto educationUserRequestDto)
        {
            if (educationUserRequestDto.RequestIds == null || educationUserRequestDto.RequestIds.Count == 0)
            {
                return BadRequest("En az bir adet talep seçiniz.");
            }
            var requests = _context.EducationUsers.Where(eu => educationUserRequestDto.RequestIds.Contains(eu.Id)).ToList();
            foreach (var request in requests)
            {
                if (educationUserRequestDto.Approve)
                {
                    if (request.JoinRequestStatus == RequestStatus.Pending)
                        request.JoinRequestStatus = RequestStatus.Approved;
                    if (request.LeaveRequestStatus == RequestStatus.Pending)
                        request.LeaveRequestStatus = RequestStatus.Approved;
                }
                else
                {
                    if (request.JoinRequestStatus == RequestStatus.Pending)
                        request.JoinRequestStatus = RequestStatus.Rejected;
                    if (request.LeaveRequestStatus == RequestStatus.Pending)
                        request.LeaveRequestStatus = RequestStatus.Rejected;
                }
                _educationUserService.TUpdate(request);
            }
            return Ok("Talepler güncellendi.");
        }


    }
}
