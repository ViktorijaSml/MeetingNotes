using MeetingNotes.Data;
using MeetingNotes.Models;
using MeetingNotes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MeetingNotes.Services
{
    public interface IManagerService
    {
        Manager? GetManagerById(int? id);
        int CreateManager(Manager manager);
        public  Task<int> CreateManagerView(CreateManagerModel model);
        IEnumerable<Manager> GetManagers();
        public IEnumerable<ManagerViewModel> GetAllManagersViewModel();
        void DeleteManager(int id);
        public bool CheckManager(int id);
        public Manager UpdateManager(Manager manager);

    }

    //---------------------------------------------------------------------------------------------------------

    public class ManagerService : IManagerService
    {
        private readonly ApplicationDbContext _db;

        public ManagerService(ApplicationDbContext db)
        {
            _db = db;
        }

//---------------------------------------------------------------------------------------------------------

        public Manager? GetManagerById(int? id)
        {
            var manager = _db.Managers.Where(w => w.ManagerId == id).FirstOrDefault();
            return manager;
        }
        public int CreateManager(Manager manager)
        {
            _db.Managers.Add(manager);
            _db.SaveChanges();
            return manager.ManagerId;
        }
        public async Task<int> CreateManagerView(CreateManagerModel model)
        {
            var manager = new Manager
            {
                ManagerId = model.SelectedManagerId,
                Workers = model.SelectedWorkers
            };
            _db.Managers.Add(manager);
            await _db.SaveChangesAsync();

            return manager.ManagerId;
        }

        public IEnumerable<Manager> GetManagers()
        {
            return _db.Managers.ToList();
        }
        public IEnumerable<ManagerViewModel> GetAllManagersViewModel()
        {
            var result = _db.Workers.Where(w => w.IsManager).Select(s => new ManagerViewModel
            {
                Id = s.Id,
                HiringDate = s.HiringDate,
                FirstName = s.Name,
                LastName = s.LastName
            }).ToList();

            return result;
        }

        public void DeleteManager(int id)
        {
            var manager = GetManagerById(id);
            _db.Managers.Remove(manager);
            _db.SaveChanges();
        }

        public Manager UpdateManager(Manager manager)
        {
            _db.Managers.Update(manager);
            _db.SaveChanges();
            return manager;
        }

        public bool CheckManager(int id) => (_db.Managers?.Any(e => e.ManagerId == id)).GetValueOrDefault();

    }
}