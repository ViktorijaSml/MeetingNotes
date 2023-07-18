﻿using Microsoft.AspNetCore.Identity;

namespace MeetingNotes.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        public bool IsManager { get; set; } = false;
        public string UserId { get; set; } = "0";  //FK usera
        public IdentityUser? identityUser { get; set; }

    }
}
