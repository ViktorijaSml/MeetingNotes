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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Azure.Identity;

namespace MeetingNotes.Controllers
{
    //[Authorize(Roles = "Worker")]

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
          //  Content("Worker");
            var workers =  _workerService.GetAllWorkers();
            return (workers !=  null) ? View(workers) : Problem("Entity set 'ApplicationDbContext.Workers'  is null.");  
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var workers =  _workerService.GetAllWorkers();

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
        public async Task<IActionResult> Create(Worker worker, string username, string email)
        {
            
            Console.WriteLine(username + " " + email);
            var identity = _workerService.CreateIdentity(username, email);
            worker.UserId = identity.Id;
            worker.identityUser = identity;
            if (worker.identityUser != null) { 
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
        public async Task<IActionResult> Edit(int id, Worker worker, string newUsername, string newEmail)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _workerService.UpdateWorker(worker, newUsername, newEmail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_workerService.CheckWorker(id))
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
                var identity = _workerService.GetIdentityUserById(worker.UserId);
                _workerService.DeleteWorker(worker);  
                _workerService.DeleteIdentity(identity);
  
            }
          
            return RedirectToAction(nameof(Index));
        }
    }
}
