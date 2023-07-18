using MeetingNotes.Data;
using MeetingNotes.Models;
using MeetingNotes.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace MeetingNotes.Services
{
    public interface IWorkerService
    {   
        public IEnumerable<Worker> GetAllWorkers();
        public Worker? GetWorkerById(int? id);
        public Worker UpdateWorker(Worker newWorker, string newUsername, string newEmail);
        public void DeleteWorker(Worker worker);
        public bool CheckWorker(int id);
        public void setManager(int workerId);
        public IdentityUser CreateIdentity(string username, string password);
        public Worker CreateWorker(Worker worker);
        public IdentityUser GetIdentityUserById(string id);
        public void DeleteIdentity(IdentityUser identity);


    }

    //---------------------------------------------------------------------------------------------------------

    public class WorkerService : IWorkerService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public WorkerService(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager; 
        }

        //---------------------------------------------------------------------------------------------------------

        public IdentityUser CreateIdentity(string username, string email)
        {
            var identityUser = new IdentityUser
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
            };
            _userManager.CreateAsync(identityUser);
            return identityUser;
        }
        public Worker CreateWorker(Worker worker)
        {                      
            _db.Workers.Add(worker);
            _db.SaveChanges();
            return worker;
        }

        public IEnumerable<Worker> GetAllWorkers() =>  _db.Workers.ToList();

        public Worker? GetWorkerById(int? id) =>  _db.Workers.Where(w => w.Id == id).Include(s => s.identityUser).FirstOrDefault();
        public IdentityUser GetIdentityUserById(string id) => _userManager.Users.FirstOrDefault(u => u.Id == id);

        public Worker UpdateWorker (Worker newWorker, string newUsername, string newEmail)
        {
            
            _db.Update(newWorker);
            var identityUser = GetIdentityUserById(newWorker.UserId);
            if (identityUser != null)
            {
                identityUser.UserName = newUsername;
                identityUser.Email = newEmail;
            }
            _db.SaveChanges();
            return newWorker;
        }

        public void DeleteWorker(Worker worker)
        {
            if (worker == null) return;
            _db.Workers.Remove(worker);
            _db.SaveChanges();
        }
      public void DeleteIdentity (IdentityUser identity)
        {
            _userManager.DeleteAsync(identity);
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
    
