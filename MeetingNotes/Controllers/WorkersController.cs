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
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity;

namespace MeetingNotes.Controllers
{
    public class WorkersController : Controller
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;

        }

        //---------------------------------------------------------------------------------------------------------

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            var  workers = await _workerService.GetAllWorkers();
            return (workers !=  null) ? View(workers) : Problem("Entity set 'ApplicationDbContext.Workers'  is null.");  
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var workers = await _workerService.GetAllWorkers();

            if (id == null || workers == null)
            {
                return NotFound();
            }

            var workerById = _workerService.GetWorkerById(id);
            if (workerById == null)
            {
                return NotFound();
            }

            return View(workerById);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LastName,HiringDate,UserId,IsManager")] Worker worker)
        {
            
            try
            { 
            if (ModelState.IsValid)
            {
                _workerService.CreateWorker(worker);
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(worker);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _workerService.GetAllWorkers() == null)
            {
                return NotFound();
            }

            var worker = _workerService.GetWorkerById(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        //---------------------------------------------------------------------------------------------------------

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,HiringDate,UserId")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _workerService.UpdateWorker(worker);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _workerService.GetAllWorkers == null)
            {
                return NotFound();
            }

            var worker = _workerService.GetWorkerById(id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_workerService.GetAllWorkers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Workers'  is null.");
            }
            var worker = _workerService.GetWorkerById(id);
            if (worker != null)
            {
                _workerService.DeleteWorker(worker);   
            }
          
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id) => _workerService.CheckWorker(id);
    }
}
