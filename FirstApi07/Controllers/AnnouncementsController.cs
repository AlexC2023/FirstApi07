using FirstApi07.DTOs;
using FirstApi07.DTOs.CreateUpdateObjects;
using FirstApi07.DTOs.PatchObject;
using FirstApi07.Helpers;
using FirstApi07.Models;
using FirstApi07.Services;
using FSharp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Net;

namespace FirstApi07.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementsService _announcementsService;
        private readonly ILogger<AnnouncementsController> _logger;

        public AnnouncementsController(IAnnouncementsService announcementsService, ILogger<AnnouncementsController> logger)
        {
            _announcementsService = announcementsService;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("GetAnnouncements started");
                var announcements = await _announcementsService.GetAnnouncementsAsync();
                if (announcements == null || !announcements.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent, "No element");
                }
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllAnnouncements error: {ex.Message}");
                return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [HttpGet("{id}")]   //idul setat aici ca nume trebui sa fie 1 la 1 cu id-ul de mai jos
        public async Task<IActionResult> GetAnnouncementAsync([FromRoute] Guid id)  // id-ul de aici
        {
            try
            {
                _logger.LogInformation("GetAnnouncementbyID started");
                var announcement = await _announcementsService.GetAnnouncementsByIdAsync(id);
                if (announcement == null)
                {
                    //return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAnnouncementByID error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> PostAnnouncementAsync([FromBody] Announcement announcement)
        {
            try
            {
                _logger.LogInformation("CreateannouncementAsync started");
                if (announcement == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _announcementsService.CreateAnnouncementAsync(announcement);
                return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncementAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("Delete Announcement Started");
                bool result = await _announcementsService.DeleteAnnouncementAsync(id);
                if (result)
                {
                    return Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted);
                }
                return BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute] Guid id, [FromBody] CreateUpdateAnnouncement announcement)
        {
            try
            {
                if (announcement == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                CreateUpdateAnnouncement updatedAnnouncement = await _announcementsService.UpdateAnnouncementAsync(id, announcement);
                if (updatedAnnouncement == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAnnouncement([FromRoute] Guid id, [FromBody] PatchAnnouncement announcement)
        {
            try
            {
                _logger.LogInformation("UpdateStarted");
                if (announcement == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                PatchAnnouncement updatedAnnouncement = await _announcementsService.UpdatePartiallyAnnouncementAsync(id, announcement);
                if (updatedAnnouncement == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
