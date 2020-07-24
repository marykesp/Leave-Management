using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AspNetCore;
using AutoMapper;
using Leave_Management.Contracts;
using Leave_Management.Data;
using Leave_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Leave_Management.Controllers
{
    [Authorize(Roles = "Administrator")]     //This ensures that someone cannot directly navigate to these pages if they are not logged in as Admin
                                             //If Access should be different on different pages, you can put this above each Controller
                                             //You can also allow multiple roles e.g. (Roles = "Administrator", "Employee")
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _leaverepo;                    //Dependency injection below
        private readonly ILeaveAllocationRepository _allocaionrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public LeaveAllocationController(ILeaveTypeRepository leaverepo, ILeaveAllocationRepository allocationrepo, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _leaverepo = leaverepo;
            _allocaionrepo = allocationrepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocationController
        public ActionResult Index()
        {
            var leaveTypes = _leaverepo.FindAll().ToList();                                             //This happens in the data layer.  Now you need to convert the results into the ViewModel
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<DetailsLeaveTypeVM>>(leaveTypes);     //Maps from the Data table class to the ViewModel
            var model = new SetLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };
            return View(model);
        }

        //Custom Action to Set Leave Days
        public ActionResult SetLeave(int id)
        {
            var leavetype = _leaverepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var nrUpdated = 0;
            foreach (var emp in employees)
            {
                if (_allocaionrepo.CheckAllocation(id, emp.Id))
                {
                    continue;
                }

                var allocation = new CreateLeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = leavetype.Id,
                    NumberOfDays = leavetype.DefaultDays,
                    LeavePeriod = DateTime.Now.Year
                };
                
                var leaveallocation = _mapper.Map<LeaveAllocation>(allocation);
                _allocaionrepo.Create(leaveallocation);
                nrUpdated++;
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }

        // GET: LeaveAllocationController/Details/5
        public ActionResult Details(string id)
        {
            var emp = _userManager.FindByIdAsync(id).Result;
            var employee = _mapper.Map<EmployeeVM>(emp);
            var allocations = _mapper.Map<List<DetailLeaveAllocationVM>>(_allocaionrepo.GetLeaveAllocationsByEmployee(id).ToList());
            var model = new ViewAllocationsVM
                { 
                    Employee = employee,
                    LeaveAllocations = allocations
                };
            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveallocation = _allocaionrepo.FindById(id);
            var model = _mapper.Map<EditEmployeeAllocationVM>(leaveallocation);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditEmployeeAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = _allocaionrepo.FindById(model.Id);
                //var allocation = _mapper.Map<LeaveAllocation>(model);  Mapping did not work perfectly, so we had to map manually.
                record.NumberOfDays = model.NumberOfDays;
                var isSuccess = _allocaionrepo.Update(record);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error while saving");
                    return View(model);
                }
                    
                return RedirectToAction(nameof(Details),new { id = model.EmployeeId});
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
