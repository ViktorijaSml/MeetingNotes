using MeetingNotes.Data;
using MeetingNotes.Models;
using System.Numerics;

namespace MeetingManager.Services
{
    public interface IManagerService
    {
        Manager? GetManagerById(int? id);
        int CreateManager(Manager manager);
        IEnumerable<Manager> GetManagers();
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
        public IEnumerable<Manager> GetManagers()
        {
            return _db.Managers.ToList();
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