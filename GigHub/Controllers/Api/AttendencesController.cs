using GigHub.Data;
using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Controllers.Api
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendencesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public AttendencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Attend(AttendenceDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (exist)
            {
                return BadRequest("Attendancce already exists");
            }

            var attendence = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendence);
            return Ok();
        }
    }
}
