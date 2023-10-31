
namespace MeetingNotes.Models
{
    public class Manager
    {

        public int ManagerId { get; set; }//ovo je zapravo WorkerId jer je manager zapravo worker

        public int WorkerId { get; set; } //da znam koji je to worker
    }
}
