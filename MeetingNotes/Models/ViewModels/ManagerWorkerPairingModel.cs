namespace MeetingNotes.Models.ViewModels
{
    public class ManagerWorkerPairingModel
    {
        public int SelectedManagerId { get; set; }
        public List<int> SelectedWorkerIds { get; set; }

        public List<WorkerSelectionViewModel> Managers { get; set; }
        public List<WorkerSelectionViewModel> Workers { get; set; }
    }
}
