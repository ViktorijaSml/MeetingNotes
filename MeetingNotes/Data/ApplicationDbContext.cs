using MeetingNotes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Manager> Managers{ get; set; }
        public DbSet<Meeting> Meetings{ get; set; } 
        public DbSet<Note> Notes{ get; set; }

        //---------------------------------------------------------------------------------------------------------

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Worker>().ToTable("Worker");
            builder.Entity<Manager>().ToTable("Manager");
            builder.Entity<Meeting>().ToTable("Meeting");
            builder.Entity<Note>().ToTable("Note");

            base.OnModelCreating(builder);
        }
}
}