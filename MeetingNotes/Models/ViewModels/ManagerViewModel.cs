using System.Runtime.ExceptionServices;

namespace MeetingNotes.Models.ViewModels
{
    public class ManagerViewModel
    {   
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
