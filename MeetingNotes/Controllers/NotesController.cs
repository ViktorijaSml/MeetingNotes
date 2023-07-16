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
    public class NotesController : Controller
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var notes = _noteService.GetAllNotes();
            return (notes != null) ? View(notes) : Problem("Entity set 'ApplicationDbContext.Meetings  is null.");
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _noteService.GetAllNotes() == null)
            {
                return NotFound();
            }

            var note = _noteService.GetNoteById(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,NoteData,Topic")] Note note)
        {
            if (ModelState.IsValid)
            {
               _noteService.CreateNote(note);
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _noteService.GetAllNotes() == null)
            {
                return NotFound();
            }

            var note = _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,NoteData,Topic")] Note note)
        {
            if (id != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _noteService.UpdateNote(note);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_noteService.CheckNote(id))
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
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _noteService.GetAllNotes() == null)
            {
                return NotFound();
            }

            var note = _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_noteService.GetAllNotes() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Note'  is null.");
            }
            var note =_noteService.GetNoteById(id);
            if (note != null)
            {
               _noteService.DeleteNote(note);
            }
            
            return RedirectToAction(nameof(Index));
        }

       
    }
}
