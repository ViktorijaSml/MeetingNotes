namespace MeetingNotes.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }
}
