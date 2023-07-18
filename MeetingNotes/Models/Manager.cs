using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotes.Models
{
    public class Manager
    {
        
        public int ManagerId { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }
}
