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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Worker>().ToTable("Worker");
            base.OnModelCreating(builder);
        }

        public DbSet<MeetingNotes.Models.Manager> Manager { get; set; } = default!;

        public DbSet<MeetingNotes.Models.Meeting> Meeting { get; set; } = default!;

        public DbSet<MeetingNotes.Models.Note> Note { get; set; } = default!;
    }
}