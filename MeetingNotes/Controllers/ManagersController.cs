using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingNotes.Data;
using MeetingNotes.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MeetingManager.Services;

namespace MeetingNotes.Controllers
{
    [Authorize(Roles = "Manager, Admin")]

    public class ManagersController : Controller
    {
        private readonly IManagerService _managerService;
        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

//---------------------------------------------------------------------------------------------------------

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            Content("Manager || Admin");
            return _managerService.GetManagers() != null ? 
                          View(_managerService.GetManagers()) :
                          Problem("Entity set 'ApplicationDbContext.Manager'  is null.");
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _managerService.GetManagers == null)
            {
                return NotFound();
            }

            var manager = _managerService.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,FirstName,LastName")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _managerService.CreateManager(manager);
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _managerService.GetManagers == null)
            {
                return NotFound();
            }

            var manager = _managerService.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,FirstName,LastName")] Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _managerService.UpdateManager(manager);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_managerService.CheckManager(manager.ManagerId))
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
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _managerService.GetManagers() == null)
            {
                return NotFound();
            }

            var manager = _managerService.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_managerService.GetManagers() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Manager'  is null.");
            }
            var manager = _managerService.GetManagerById(id);
            if (manager != null)
            {
                _managerService.DeleteManager(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
