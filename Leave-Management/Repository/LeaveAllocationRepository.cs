using Leave_Management.Contracts;
using Leave_Management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository  //Each repository has to be added in Startup.cs
    {
        private readonly ApplicationDbContext _db;                      //This is to let the repository know about the database;

        public LeaveAllocationRepository(ApplicationDbContext db)       //ctor to generate constructor
        {
            _db = db;                                                   //Initialise the private variable with the db value received.  Dependency injection;
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            var leaveAllocations = _db.LeaveAllocations.ToList();
            return leaveAllocations;
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocations.Find();
            return leaveAllocation;
        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveAllocations.Any(c => c.Id == id);
            return exists;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.Update(entity);
            return Save();
        }
    }
}
