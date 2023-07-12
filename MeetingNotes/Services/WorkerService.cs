using MeetingNotes.Data;
using MeetingNotes.Models;
using MeetingNotes.Controllers;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotes.Services
{
    public interface IWorkerService
    {   
        public Task<IEnumerable<Worker>> GetAllWorkers();
        public Worker? GetWorkerById(int? id);
        public int CreateWorker(Worker worker);
        public Worker UpdateWorker(Worker worker);
        public void DeleteWorker(Worker worker);
        public bool CheckWorker(int id);
        public void setManager(int workerId);
    }

    //---------------------------------------------------------------------------------------------------------

    public class WorkerService : IWorkerService
    {
        private readonly ApplicationDbContext _db;
        public WorkerService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Worker>> GetAllWorkers() => await _db.Workers.ToListAsync();

        public Worker? GetWorkerById(int? id) =>  _db.Workers.Where(w => w.Id == id).FirstOrDefault();

          
        public int CreateWorker(Worker worker)
        {
            _db.Workers.Add(worker);//ide u kvazi cache
            _db.SaveChanges();// sprema u bazu, jako bitno
            return worker.Id;
        }

        public Worker UpdateWorker (Worker worker)
        {
            _db.Update(worker);
            _db.SaveChanges();
            return worker;
        }

        public void DeleteWorker(Worker worker)
        {
            _db.Workers.Remove(worker);
            _db.SaveChanges();
        }

        public void setManager(int workerId) {
            var worker = GetWorkerById(workerId);
            
            if (worker != null)
            {
                worker.IsManager = true;
                _db.SaveChanges();
            }
        }

        public bool CheckWorker(int id) => (_db.Workers?.Any(e => e.Id == id)).GetValueOrDefault();

    }
}
    
