using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingNotes.Models;
using MeetingNotes.Services;
using MeetingNotes.Models.ViewModels;

namespace MeetingNotes.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IMeetingService _meetingService;
        private readonly INoteService _noteService;
        public MeetingsController(IMeetingService meetingService, INoteService noteService)
        {
            _meetingService = meetingService;
            _noteService = noteService; 
        }

    //---------------------------------------------------------------------------------------------------------

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            var meetings = _meetingService.GetAllMeetingsViewModel();
            return (meetings != null) ? View(meetings) : Problem("Entity set 'ApplicationDbContext.Meetings  is null.");
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _meetingService.GetAllMeetings() == null)
            {
                return NotFound();
            }

            var meeting = _meetingService.GetMeetingById(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            var model = _meetingService.GetMeetingCreateViewModel();
            return View(model);
        }

        // POST: Meetings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingCreateModel model)
        {   //popravi note i radice
            if(ModelState.IsValid)
            {
               
                await _meetingService.CreateMeeting(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _meetingService.GetAllMeetings() == null)
            {
                return NotFound();
            }

            var meeting =_meetingService.GetMeetingById(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Meeting meeting)
        {
            if (meeting.MeetingId == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _meetingService.UpdateMeeting(meeting);
                    var meetings = _meetingService.GetMeetingById(meeting.MeetingId);
                    _noteService.UpdateNote(meetings.Note);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_meetingService.CheckMeeting(meeting.MeetingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _meetingService.GetAllMeetings() == null)
            {
                return NotFound();
            }

            var meeting = _meetingService.GetMeetingById(id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_meetingService.GetAllMeetings() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Meeting'  is null.");
            }
            var meeting = _meetingService.GetMeetingById(id);
            if (meeting != null)
            {
                _meetingService.DeleteMeeting(meeting);
                _noteService.DeleteNote(meeting.Note);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult GetWorkers(int managerId)
        {
            var workers = _meetingService.GetWorkersByManager(managerId);
            var jsonedWorkers = Json(workers);
            return jsonedWorkers;
        }
    }
}
