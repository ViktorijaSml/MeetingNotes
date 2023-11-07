namespace MeetingNotes.Models.ViewModels
{
    public class ManagerEditViewModel
    {//I will only let to change workers here
     //if you want to change details about the manager itself, it should be done in workers window
        public int ManagerId { get; set; }
        public List<int> SelectedWorkerIds { get; set; }
        public List<WorkerSelectionViewModel> ManagersWorkers { get; set; }
        public List<WorkerSelectionViewModel> OtherWorkers { get; set; }


    }
}
