namespace MeetingNotes.Models.ViewModels
{
    public class ManagerWorkerPairingModel
    {
        public int SelectedManagerId { get; set; }
        public List<int> SelectedWorkerIds { get; set; }

        public List<WorkerSelectionViewModel> Managers { get; set; }//onda od ovoga selectam jedan id
        public List<WorkerSelectionViewModel> Workers { get; set; }//onda od ovoga checkboxam vise id
    }
}
