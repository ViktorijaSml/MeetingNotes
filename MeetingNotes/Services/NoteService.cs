using MeetingNotes.Data;
using MeetingNotes.Models;

namespace MeetingNotes.Services
{
    public interface INoteService {
        public IEnumerable<Note> GetAllNotes();
        public Note? GetNoteById(int? id);
        public int CreateNote(Note note);
        public Note UpdateNote(Note note);
        public void DeleteNote(Note note);
        public bool CheckNote(int id);
    }

//---------------------------------------------------------------------------------------------------------

    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _db;
        public NoteService(ApplicationDbContext db)
        {
            _db = db;
        }

//---------------------------------------------------------------------------------------------------------

        public IEnumerable<Note> GetAllNotes() => _db.Notes.ToList();
        public Note? GetNoteById(int? id) => _db.Notes.Where(w => w.NoteId == id)
                                                      .FirstOrDefault();
        public int CreateNote(Note note) { 
            _db.Add(note);
            _db.SaveChanges();
            return note.NoteId; 
        }
        public Note UpdateNote(Note note)
        {
            _db.Update(note);
            _db.SaveChanges();
            return note;
        }
        public void DeleteNote(Note note)
        {
            _db.Notes.Remove(note);
            _db.SaveChanges();
        }
        public bool CheckNote(int id) => (_db.Meetings?.Any(e => e.MeetingId == id)).GetValueOrDefault();
    }
}
