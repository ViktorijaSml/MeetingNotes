namespace MeetingNotes.Models.ViewModels
{
    public class ManagerDetailsViewModel
    {
        public int ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
