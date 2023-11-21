namespace MeetingNotes.Models.ViewModels
{
    public class MeetingCreateModel
    {
        public int SelectedManagerId { get; set; }
        public int SelectedWorkerId { get; set; }
        public DateTime MeetingDate { get; set; }
        public Note Note { get; set; }
    }
}
