namespace MeetingNotes.Models.ViewModels
{
    public class ManagerDeleteViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        public List<int> WorkerIds { get; set; }
    }
}
