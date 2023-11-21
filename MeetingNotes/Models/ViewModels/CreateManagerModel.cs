namespace MeetingNotes.Models.ViewModels
{
    public class CreateManagerModel
    {
        public int SelectedManagerId { get; set; }
        public List<int> SelectedWorkerIds { get; set; }
    }
}
