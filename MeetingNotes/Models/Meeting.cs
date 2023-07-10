namespace MeetingNotes.Models
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public int ManagerId { get; set; }
        public int WorkerId { get; set; }
        public int NotesId { get; set; }
        public DateTime DateTime { get; set; }

    }
}
