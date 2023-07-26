using System.ComponentModel.DataAnnotations;

namespace MeetingNotes.Models.ViewModels
{
    public class CreateManagerModel
    {
        public int SelectedManagerId { get; set; } //odabrani manager

        [Display(Name = "Select Manager")]
        public ICollection<Worker> Managers { get; set; } //odakle odabirat managera

        public ICollection<Worker> SelectedWorkers{ get; set; }

        [Display(Name = "Select Worker without Manager")]
        public IEnumerable<Worker> AvailableWorkers { get; set; } //odakle 
    }
}
