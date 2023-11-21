namespace MeetingNotes.Models.ViewModels
{
    public class MeetingCreateViewModel
    {
        public List<WorkerSelectionViewModel> Managers { get; set; }
        public List<WorkerSelectionViewModel> Workers { get; set; }
        public int SelectedManagerId { get; set; }
        public int SelectedWorkerId { get; set; }
        public DateTime MeetingDate { get; set; }
        public Note Note { get; set; }
    }
}
