using GigHub.Data;
using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingsController : ControllerBase
    {
        private ApplicationDbContext _context;

        public FollowingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Follow(FollowingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = _context.Followings.Any(a => a.FolloweeId == userId && a.FolloweeId == dto.FolloweeId);

            if (exist)
            {
                return BadRequest("Following already exists");
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
