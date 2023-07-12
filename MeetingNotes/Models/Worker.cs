using Microsoft.AspNetCore.Identity;

namespace MeetingNotes.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        public bool IsManager { get; set; } = false;
        public Guid UserId { get; set; } //FK usera
        public IdentityUser? IdentityUser { get; set; }

    }
}
