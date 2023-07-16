using MeetingNotes.Data;
using MeetingNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotes.Services
{
    public interface IMeetingService {
        public IEnumerable<Meeting> GetAllMeetings();
        public Meeting? GetMeetingById(int? id);
        public int CreateMeeting(Meeting meeting);
        public Meeting UpdateMeeting(Meeting meeting);
        public void DeleteMeeting(Meeting meeting);
        public bool CheckMeeting(int id);
    }

    //---------------------------------------------------------------------------------------------------------

    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _db;
        public MeetingService(ApplicationDbContext db)
        {
            _db = db;
        }

        //---------------------------------------------------------------------------------------------------------


        public IEnumerable<Meeting> GetAllMeetings() => _db.Meetings.ToList();

        public Meeting? GetMeetingById(int? id)
        {
            var meeting = _db.Meetings.Where(w => w.MeetingId == id).Include(s => s.Note).FirstOrDefault();
            return meeting;
        }

        public int CreateMeeting(Meeting meeting) {
            _db.Add(meeting);
            _db.SaveChanges();
            return meeting.MeetingId;
        }

        public Meeting UpdateMeeting(Meeting meeting) {
            _db.Update(meeting);
            _db.SaveChanges();
            return meeting;
        }

        public void DeleteMeeting(Meeting meeting) {
            _db.Meetings.Remove(meeting);
            _db.SaveChanges();
        }
        public bool CheckMeeting(int id) => (_db.Meetings?.Any(e => e.MeetingId == id)).GetValueOrDefault();
    }
}
