using Leave_Management.Contracts;
using Leave_Management.Data;
using Microsoft.EntityFrameworkCore;
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

        public bool CheckAllocation(int leavetypeid, string employeeid)  //Check if any records already exist
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == employeeid
                                && q.LeaveTypeId == leavetypeid
                                && q.LeavePeriod == period)
                            .Any();
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
            var leaveAllocations = _db.LeaveAllocations
                                    .Include(q => q.LeaveType)   //This will make sure we return the related Leave Type details as well. We need it for the UI
                                    .Include(c => c.Employee)    //Before including these we got a Null Ref exception when using these fields
                                    .ToList();
            return leaveAllocations;
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocations
                                    .Include(q => q.LeaveType)
                                    .Include(c => c.Employee)
                                    .FirstOrDefault(q => q.Id == id);

            return leaveAllocation;
        }

        public ICollection<LeaveAllocation> FindEmployeeAllocationsById(int id)
        {
            var leaveAllocations = _db.LeaveAllocations
                                   .Where(q => q.Id == id)
                                   .Include(c => c.Employee)
                                   .ToList();
            return leaveAllocations;
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            var allocations = FindAll().Where(q => q.EmployeeId == id && q.LeavePeriod == period)
                .ToList();
            return allocations;
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
