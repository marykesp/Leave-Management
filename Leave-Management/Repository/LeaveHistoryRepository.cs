using Leave_Management.Contracts;
using Leave_Management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository   //Each repository has to be added in Startup.cs
    {
        private readonly ApplicationDbContext _db;                  //This is to let the repository know about the database;

        public LeaveHistoryRepository(ApplicationDbContext db)      //ctor to generate constructor
        {
            _db = db;                                               //Initialise the private variable with the db value received.  Dependency injection;
        }

        public bool Create(LeaveHistory entity)
        {
            _db.Add(entity);
            return Save();
        }

        public bool Delete(LeaveHistory entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveHistory> FindAll()
        {
            var leaveHistories = _db.LeaveHistories.ToList();
            return leaveHistories;
        }

        public LeaveHistory FindById(int id)
        {
            var leaveHistory = _db.LeaveHistories.Find(id);
            return leaveHistory;
        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveHistories.Any(c => c.Id == id);
            return exists;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveHistory entity)
        {
            _db.Update(entity);
            return Save();
        }
    }
}
