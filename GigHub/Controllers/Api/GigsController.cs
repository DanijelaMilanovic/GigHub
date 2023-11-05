using GigHub.Data;
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
    public class GigsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public GigsController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        [HttpDelete]
        public IActionResult Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if(gig.IsCanceled)
            {
                return NotFound();
            }

            gig.IsCanceled = true;
            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCanceled
            };

            var attendies = _context.Attendances
                .Where(a=>a.GigId == gig.Id)
                .Select(a=>a.Attendee)
                .ToList();    

            foreach (var atendee in attendies)
            {
                var userNotification = new UserNotification
                {
                    User = atendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
                
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
