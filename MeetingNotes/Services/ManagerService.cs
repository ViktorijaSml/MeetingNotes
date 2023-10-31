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
        public ManagerWorkerPairingModel GetManagerWorkerPairingModel();

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
            //This is also for the CreateView, getting selected Managers
            try
            {
                //Creating a new Manager with original Manager model and saving his workers
                List<Manager> managers = model.SelectedWorkerIds.Select(workerId => new Manager
                {
                    ManagerId = model.SelectedManagerId,
                    WorkerId = workerId
                }).ToList();

                //Adding managers to the database
                foreach (var manager in managers)
                {
                    await _db.AddRangeAsync(managers);                
                }
                //ne treba stalno save changes, dovoljan je jedan za sve
                await _db.SaveChangesAsync();

                return model.SelectedManagerId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    public IEnumerable<Manager> GetManagers()
        {
            return _db.Managers.ToList();
        }
        public IEnumerable<ManagerViewModel> GetAllManagersViewModel()
        {
            var managerIDs = _db.Managers.Select(w => w.ManagerId).ToList();
            var result = _db.Workers.Where(w => managerIDs.Contains(w.Id)).Select(s => new ManagerViewModel
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
        public ManagerWorkerPairingModel GetManagerWorkerPairingModel()
        {   //This is for CreateView, we need 2 dropdown lists

            //Creating Workers and adding values
            var workers = _db.Workers.Select(w => new WorkerSelectionViewModel
                {
                    Id = w.Id,
                    FullName = w.Name + " " + w.LastName
                }).ToList();

            //Creating Managers using WorkerModel
            var managers = _db.Workers.Where(s => s.IsManager).Select(w => new WorkerSelectionViewModel
            {
                Id = w.Id,
                FullName = w.Name + " " + w.LastName
            }).ToList();

            //Pairing Managers and Workers into one ViewModel
            var viewModel = new ManagerWorkerPairingModel
            {
                Managers = managers,
                Workers = workers,
            };

            return viewModel;
        }

    }
}