﻿using MeetingNotes.Data;
using MeetingNotes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MeetingNotes.Models.ViewModels;


namespace MeetingNotes.Services
{
    public interface IWorkerService
    {   
        public IEnumerable<Worker> GetAllWorkers();
        public IEnumerable<WorkerViewModel> GetAllWorkersViewModel();
        public Worker? GetWorkerById(int? id);
        public Worker UpdateWorker(Worker newWorker, string newUsername, string newEmail);
        public void DeleteWorker(Worker worker);
        public bool CheckWorker(int id);
        public void setManager(int workerId);
        public Worker CreateWorker(Worker worker);
        public Task<int> CreateWorkerView(CreateWorkerViewModel model);
        public WorkerDetailsModel GetWorkerDetailsById(int? id);
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
       
        public Worker CreateWorker(Worker worker)
        {                      
            _db.Workers.Add(worker);
            _db.SaveChanges();
            return worker;
        }
        public async Task<int> CreateWorkerView(CreateWorkerViewModel model)
        {
            var modifiedEmail = model.Email.Replace('@', ' ');

            try
            {
                //Creating IdentityUser
                var user = new IdentityUser
                        {
                            UserName = modifiedEmail,
                            NormalizedUserName = modifiedEmail.ToUpper(),
                            Email = model.Email,
                            NormalizedEmail = model.Email.ToUpper(),
                            EmailConfirmed = true   
                        };
               

                PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
                user.PasswordHash = ph.HashPassword(user, model.Password);

                await _userManager.CreateAsync(user);

                //Adding role to user
                if (model.IsManager == true) 
                { 
                    await _userManager.AddToRoleAsync(user, "Manager");
                }
                await _userManager.AddToRoleAsync(user, "Worker");
                
                //Creating Worker
                var worker = new Worker
                        {
                            Name = model.FirstName,
                            LastName = model.LastName,
                            HiringDate = model.EnrollmentDate,
                            IsManager = model.IsManager,
                            identityUser = user,
                            UserId = user.Id
                        };

                _db.Workers.Add(worker);
                _db.SaveChanges();

                return worker.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IEnumerable<Worker> GetAllWorkers() =>  _db.Workers.ToList();
        public IEnumerable<WorkerViewModel> GetAllWorkersViewModel()
        {
            var result = _db.Workers.Select(s => new WorkerViewModel
                                            {
                                                Id = s.Id,
                                                HiringDate = s.HiringDate,
                                                FirstName = s.Name,
                                                LastName = s.LastName
                                            }).ToList();
            return result;
        }
        public WorkerDetailsModel GetWorkerDetailsById (int? id)
        {
           var worker =  _db.Workers.Where(w => w.Id == id)
                                    .Include(s => s.identityUser)
                                    .FirstOrDefault();

            WorkerDetailsModel model = new WorkerDetailsModel
                                    {
                                        Id = worker.Id,
                                        FirstName = worker.Name,
                                        LastName = worker.LastName,
                                        HiringDate = worker.HiringDate,
                                        Email = worker.identityUser.Email,
                                    };
            model.IsManager = (worker.IsManager == true) ? "Yes" : "No";

            return model;
        }
        public Worker? GetWorkerById(int? id) =>  _db.Workers.Where(w => w.Id == id)
                                                             .Include(s => s.identityUser)
                                                             .FirstOrDefault();
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
        public void DeleteIdentity (IdentityUser identity) => _userManager.DeleteAsync(identity);    
        public void setManager(int workerId) 
        {
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
    
