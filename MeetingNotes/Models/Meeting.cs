using System.ComponentModel.DataAnnotations;

namespace MeetingNotes.Models
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        [Required]
        public int ManagerId { get; set; }
        [Required]
        public int WorkerId { get; set; }
        public DateTime DateTime { get; set; }

        public Note Note { get; set; }

    }
}
