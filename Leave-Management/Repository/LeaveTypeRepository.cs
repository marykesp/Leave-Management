using Leave_Management.Contracts;
using Leave_Management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Leave_Management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository     //Each repository has to be added in Startup.cs
    {
        private readonly ApplicationDbContext _db;              //This is to let the repository know about the database;

        public LeaveTypeRepository(ApplicationDbContext db)     //ctor to generate constructor
        {
            _db = db;                                           //Initialise the private variable with the db value received.  Dependency injection;
        }

        public bool Create(LeaveType entity)
        {
            _db.LeaveTypes.Add(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            _db.LeaveTypes.Remove(entity);
            return Save();
        }

        public ICollection<LeaveType> FindAll()
        {
            var leaveTypes = _db.LeaveTypes.ToList();
            return leaveTypes;
        }

        public LeaveType FindById(int id)
        {
            var leaveType = _db.LeaveTypes.Find(id);        //Another way: _db.LeaveTypes.FirstOrDefault(with lambda expression)
            return leaveType;
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool isExists(int id)
        {
            var exists = _db.LeaveTypes.Any(c => c.Id == id);
            return exists;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;               //This checks if at least one record was updated.  Then it will return true
        }

        public bool Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return Save();
        }
    }
}
