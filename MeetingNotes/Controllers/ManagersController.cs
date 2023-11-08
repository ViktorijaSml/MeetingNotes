using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MeetingNotes.Services;
using MeetingNotes.Models.ViewModels;

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
            return _managerService.GetAllManagersViewModel() != null ? 
                          View(_managerService.GetAllManagersViewModel()) :
                          Problem("Entity set 'ApplicationDbContext.Manager'  is null.");
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _managerService.GetManagers == null)
            {
                return NotFound();
            }

            var manager = _managerService.GetManagerDetailsView(id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View(_managerService.GetManagerWorkerPairingModel());
        }

        // POST: Managers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateManagerModel model)
        {
            if (ModelState.IsValid)
            {
                await _managerService.CreateManagerView(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _managerService.GetManagers == null)
            {
                return NotFound();
            }

            var manager = _managerService.GetManagerEditView(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateManagerModel model)
        {
            var manager = _managerService.GetManagerById(model.SelectedManagerId);

            if (ModelState.IsValid)
            {
                try
                {
                   _managerService.UpdateManager(model.SelectedManagerId, model.SelectedWorkerIds);
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

            var model = _managerService.GetManagerViewModelById(id);
            return View(model);
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
