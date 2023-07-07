using MeetingNotes.Data;
using MeetingNotes.Models;

namespace MeetingNotes.Services
{
    public interface IWorkerService
    {
        Worker? GetWorkerById(int id);
        int CreateWorker(Worker worker);
        public IEnumerable<Worker> GetAllWorkers();

    }
    public class WorkerService : IWorkerService
    {
        private readonly ApplicationDbContext _db;
        public WorkerService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Worker? GetWorkerById(int id)
        {
            //firstOrDefault vraca null ako nema vrijednosti - inace bi bio error
            var Worker = _db.Workers.Where(w => w.Id == id).FirstOrDefault();
            return Worker;
        }
        public int CreateWorker(Worker worker)
        {
            _db.Workers.Add(worker);//ide u kvazi cache
            _db.SaveChanges();// sprema u bazu, jako bitno
            return worker.Id;
        }

        public IEnumerable<Worker> GetAllWorkers()
        {
            return _db.Workers.ToList();
        }

    }
}
    
