
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        public int GigId { get; set; }
        public Gig Gig { get; set; }

        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }
    }
}
