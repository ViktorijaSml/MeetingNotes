using MeetingNotes.Data;
using MeetingNotes.Models;
using MeetingNotes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotes.Services
{
    public interface IMeetingService {
        IEnumerable<MeetingViewModel> GetAllMeetingsViewModel();
        public IEnumerable<Meeting> GetAllMeetings();
        public Meeting? GetMeetingById(int? id);
        public Task<int> CreateMeeting(MeetingCreateModel model);
        public Meeting UpdateMeeting(Meeting meeting);
        public void DeleteMeeting(Meeting meeting);
        public bool CheckMeeting(int id);
        public MeetingCreateViewModel GetMeetingCreateViewModel();
        public IEnumerable<WorkerSelectionViewModel> GetWorkersByManager(int managerId);
    }

    //---------------------------------------------------------------------------------------------------------

    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _db;
        public MeetingService(ApplicationDbContext db)
        {
            _db = db;
        }

    //---------------------------------------------------------------------------------------------------------

        public IEnumerable<MeetingViewModel> GetAllMeetingsViewModel()
        {
            var result = _db.Meetings.Select(s => new MeetingViewModel
                                            {
                                                MeetingId = s.MeetingId,
                                                MeetingDate = s.DateTime,
                                                ManagerFullName = _db.Workers.Where(w => w.Id == s.ManagerId)
                                                                             .Select(s => s.Name + " " + s.LastName)
                                                                             .FirstOrDefault(),
                                                WorkerFullName = _db.Workers.Where(w => w.Id == s.WorkerId)
                                                                            .Select(s => s.Name + " " + s.LastName)
                                                                            .FirstOrDefault()
                                            }).ToList();
            return result;
        }
        public IEnumerable<Meeting> GetAllMeetings() => _db.Meetings.ToList();
        public Meeting? GetMeetingById(int? id)
        {
            var meeting = _db.Meetings.Where(w => w.MeetingId == id)
                                      .Include(s => s.Note)
                                      .FirstOrDefault();
            return meeting;
        }
        public async Task<int> CreateMeeting(MeetingCreateModel model) 
        {
            var meeting = new Meeting()
            {
                ManagerId = model.SelectedManagerId,
                WorkerId = model.SelectedWorkerId,
                DateTime = model.MeetingDate,
                Note = model.Note
            };
            _db.Add(meeting);
            await _db.SaveChangesAsync();
            return meeting.MeetingId;
        }
        public Meeting UpdateMeeting(Meeting meeting) 
        {
            _db.Update(meeting);
            _db.SaveChanges();
            return meeting;
        }
        public void DeleteMeeting(Meeting meeting) 
        {
            _db.Meetings.Remove(meeting);
            _db.SaveChanges();
        }
        public bool CheckMeeting(int id) => (_db.Meetings?.Any(e => e.MeetingId == id)).GetValueOrDefault();
        public MeetingCreateViewModel GetMeetingCreateViewModel()
        {
            var managersId = _db.Managers.Select(s => s.ManagerId)
                                         .ToList();

            List<WorkerSelectionViewModel> managerSelectionModel = new List<WorkerSelectionViewModel>();
            List<WorkerSelectionViewModel> workerSelectionModel = new List<WorkerSelectionViewModel>();

            foreach (var worker in _db.Workers)
            {
                if (managersId.Contains(worker.Id))
                {
                    managerSelectionModel.Add(new WorkerSelectionViewModel
                                        {
                                            Id = worker.Id,
                                            FullName = worker.Name + " " + worker.LastName
                                        });
                }
                else
                {
                    workerSelectionModel.Add(new WorkerSelectionViewModel
                                        {
                                            Id = worker.Id,
                                            FullName = worker.Name + " " + worker.LastName
                                        });
                }
            }

            return new MeetingCreateViewModel
            {
                Managers = managerSelectionModel,
                Workers = workerSelectionModel
            };
        }
        public IEnumerable<WorkerSelectionViewModel> GetWorkersByManager(int managerId)
        {
            List<int> WorkersId = new List<int>();
            foreach (var manager in _db.Managers.Where(s => s.ManagerId == managerId))
            {
                WorkersId.Add(manager.WorkerId);
            }
            var workers = _db.Workers.Where(s => WorkersId.Contains(s.Id))
                              .Select(w => new WorkerSelectionViewModel
                              {
                                  FullName = w.Name + " " + w.LastName,
                                  Id = w.Id
                              }).ToList();
            return _db.Workers.Where(s => WorkersId.Contains(s.Id))
                              .Select(w => new WorkerSelectionViewModel
                                    {
                                        FullName = w.Name + " " + w.LastName,
                                        Id = w.Id
                                    }).ToList();
        }

    }
}
