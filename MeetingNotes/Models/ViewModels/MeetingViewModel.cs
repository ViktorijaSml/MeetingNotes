namespace MeetingNotes.Models.ViewModels
{
    public class MeetingViewModel
    {
        public int MeetingId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string ManagerFullName { get; set; }
        public string WorkerFullName { get; set; }
    }
}
