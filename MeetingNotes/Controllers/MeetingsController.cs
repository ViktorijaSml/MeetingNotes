using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingNotes.Data;
using MeetingNotes.Models;
using MeetingNotes.Services;


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
            var meetings = _meetingService.GetAllMeetings();
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
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,WorkerId,NotesId,DateTime")] Meeting meeting, [Bind("NotesData")] Note note)
        {
            if (ModelState.IsValid)
            {
                _meetingService.CreateMeeting(meeting);
                _noteService.CreateNote(note);
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingId,ManagerId,WorkerId,NotesId,DateTime")] Meeting meeting)
        {
            if (id != meeting.MeetingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _meetingService.UpdateMeeting(meeting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_meetingService.CheckMeeting(id))
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
            }
    
            return RedirectToAction(nameof(Index));
        }
    }
}
