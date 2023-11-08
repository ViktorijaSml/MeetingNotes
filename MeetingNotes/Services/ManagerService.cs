using MeetingNotes.Data;
using MeetingNotes.Models;
using MeetingNotes.Models.ViewModels;

namespace MeetingNotes.Services
{
    public interface IManagerService
    {
        public Manager? GetManagerById(int? id);
        public int CreateManager(Manager manager);
        public  Task<int> CreateManagerView(CreateManagerModel model);
        IEnumerable<Manager> GetManagers();
        public IEnumerable<ManagerViewModel> GetAllManagersViewModel();
        public void DeleteManager(int id);
        public bool CheckManager(int id);
        public void UpdateManager(int id, List<int> SelectedWorkerIds);
        public ManagerWorkerPairingModel GetManagerWorkerPairingModel();
        public ManagerDeleteViewModel? GetManagerViewModelById(int? id);
        public ManagerDetailsViewModel? GetManagerDetailsView(int? id);
        public ManagerEditViewModel? GetManagerEditView(int? id);
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
            var manager = _db.Managers.Where(w => w.ManagerId == id)
                                      .FirstOrDefault();
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
        public IEnumerable<Manager> GetManagers() => _db.Managers.ToList();
        public IEnumerable<ManagerViewModel> GetAllManagersViewModel()
        {
            var managerIDs = _db.Managers.Select(w => w.ManagerId)
                                         .ToList();
            var result = _db.Workers.Where(w => managerIDs.Contains(w.Id))
                                    .Select(s => new ManagerViewModel
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
            foreach(var manager in _db.Managers)
            {
                if(manager.ManagerId == id)
                {
                    _db.Managers.Remove(manager);
                }
            }
            _db.SaveChanges();
        }
        public void UpdateManager(int id, List<int> SelectedWorkerIds)
        {
            //We get a completely changed list of workers under a manager
            var workers = _db.Managers.Where(s => s.ManagerId == id);
            var workersId = workers.Select(s => s.WorkerId)
                                   .ToList();
            //check if previous worker is in the new selected list, if not it means it was unchecked, therefore remove it
            foreach (var worker in workers)
            {
                if (!SelectedWorkerIds.Contains(worker.WorkerId))
                {
                    _db.Managers.Remove(worker);
                }
            }
            //check what new workers has the selected list added, if its not in the previous list, add it
            foreach(var worker in SelectedWorkerIds)
            {
                if (!workersId.Contains(worker))
                {
                    var manager = new Manager
                            {
                                ManagerId = id,
                                WorkerId = worker
                            };
                    _db.Managers.Add(manager);
                }
            }
            _db.SaveChanges();
        }
        public bool CheckManager(int id) => (_db.Managers?.Any(e => e.ManagerId == id))
                                                          .GetValueOrDefault();
        public ManagerWorkerPairingModel GetManagerWorkerPairingModel()
        {   //This is for CreateView, we need 2 dropdown lists

            //Creating Workers and adding values
            var workers = _db.Workers.Select(w => new WorkerSelectionViewModel
                                            {
                                                Id = w.Id,
                                                FullName = w.Name + " " + w.LastName
                                            }).ToList();

            var existingManagerIDs = _db.Managers.Select(w => w.ManagerId).ToList();
            //Creating Managers using WorkerModel
            //If we have created a manager already, don't put him as an option
            var managers = _db.Workers.Where(s => !existingManagerIDs.Contains(s.Id) && s.IsManager == true)
                                      .Select(w => new WorkerSelectionViewModel
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
        public ManagerDeleteViewModel? GetManagerViewModelById(int? id)
        {
            if (_db.Managers.Where(w => w.ManagerId == id) == null)
            {
                return null;
            }

            var workers = new List<int>();
            foreach(var selectedManager in _db.Managers.Where(s => s.ManagerId == id))
            {
                workers.Add(selectedManager.WorkerId);
            }

            var result = _db.Workers.Where(w => w.Id == id)
                                    .Select(s => new ManagerDeleteViewModel
                                            {
                                                Id = s.Id,
                                                HiringDate = s.HiringDate,
                                                FirstName = s.Name,
                                                LastName = s.LastName,
                                                WorkerIds = workers
                                            }).FirstOrDefault();
            return result;
        }
        public ManagerDetailsViewModel? GetManagerDetailsView(int? id)
        {
            if (_db.Managers.Where(w => w.ManagerId == id) == null)
            {
                return null;
            }

            var workers = new List<Worker>();
            foreach(var selectedManager in _db.Managers.Where(s => s.ManagerId == id))
            {
                var worker = _db.Workers.Where(s => s.Id == selectedManager.WorkerId)
                                        .FirstOrDefault();
                workers.Add(worker);
            }

            var result = _db.Workers.Where(w => w.Id == id)
                                    .Select(s => new ManagerDetailsViewModel
                                            {
                                                ManagerId = s.Id,
                                                HiringDate = s.HiringDate,
                                                FirstName = s.Name,
                                                LastName = s.LastName,
                                                Workers = workers
                                            }).FirstOrDefault();
            return result;
        }
        public ManagerEditViewModel? GetManagerEditView(int? id)
        {
            if (_db.Managers.Where(w => w.ManagerId == id) == null)
            {
                return null;
            }

            var managersWorkers = new List<WorkerSelectionViewModel>();

            foreach (var selectedManager in _db.Managers.Where(s => s.ManagerId == id))
            {
                var worker = _db.Workers.Where(s => s.Id == selectedManager.WorkerId)
                                        .Select(w => new WorkerSelectionViewModel
                                                {
                                                    FullName = w.Name + " " + w.LastName,
                                                    Id = w.Id
                                                }).FirstOrDefault();
                managersWorkers.Add(worker);
            }

            var managersWorkerIds = _db.Managers.Where(m => m.ManagerId == id)
                                                .Select(m => m.WorkerId)
                                                .ToList();
            var otherWorkers = _db.Workers.Where(w => !managersWorkerIds.Contains(w.Id) && w.Id != id)
                                          .Select(k => new WorkerSelectionViewModel
                                                    {
                                                        FullName = k.Name + " " + k.LastName,
                                                        Id = k.Id
                                                    }).ToList();
            //ne moze first or default,to daje samo jedan rezultat kao sto i pise            

            var result = _db.Workers.Where(w => w.Id == id)
                                    .Select(s => new ManagerEditViewModel
                                            {
                                                ManagerId = s.Id,
                                                ManagersWorkers = managersWorkers,
                                                OtherWorkers = otherWorkers
                                            }).FirstOrDefault();
            return result;
        }

    }
}