﻿namespace MeetingNotes.Models.ViewModels
{
    public class WorkerDetailsModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime HiringDate { get; set; }
        public string IsManager { get; set; }
    }
}
