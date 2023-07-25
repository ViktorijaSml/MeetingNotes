namespace MeetingNotes.Models.ViewModels
{
    public class WorkerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        //ne zelim stavljat ista vise da ne bude grozno za vidit
        //ostatak info ima u detailsima
    }
}
