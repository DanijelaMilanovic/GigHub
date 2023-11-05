using GigHub.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artist = _context.Followings
                        .Where(f => f.FollowerId== userId)
                        .Select(f=>f.Followee)
                        .ToList();

            return View(artist);
        }
    }
}
