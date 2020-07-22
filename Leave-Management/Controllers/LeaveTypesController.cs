using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leave_Management.Contracts;
using Leave_Management.Data;
using Leave_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leave_Management.Controllers
{
    [Authorize(Roles = "Administrator")]     //This ensures that someone cannot directly navigate to these pages if they are not logged in as Admin
                                             //If Access should be different on different pages, you can put this above each Controller
                                             //You can also allow multiple roles e.g. (Roles = "Administrator", "Employee")
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repo;                    //Dependency injection below
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypesController
        
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();                                          //This happens in the data layer.  Now you need to convert the results into the ViewModel
            var model = _mapper.Map<List<LeaveType>, List<DetailsLeaveTypeVM>>(leaveTypes);     //Maps from the Data table class to the ViewModel
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<DetailsLeaveTypeVM>(leaveType);             //Maps the ViewModel to the database class
            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);          //This tells the system to map the received data into the LeaveType database model
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = _repo.Create(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Save was not successful");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception: {ex.Message}");
                return View();
            }
        }

        // GET: LeaveTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<DetailsLeaveTypeVM>(leaveType);             //Maps the ViewModel to the database class
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DetailsLeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);          //This tells the system to map the received data into the LeaveType database model

                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Update was not successful");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception: {ex.Message}");
                return View();
            }
        }

        //GET: LeaveTypesController/Delete/5                            //This function does the delete directly on the Index page
        public ActionResult Delete(int id)
        {
            var leaveType = _repo.FindById(id);
            if (leaveType == null)
            {
                return BadRequest();
            }
            var isSuccess = _repo.Delete(leaveType);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Delete was not successful");
                return View(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveTypesController/Delete/                        //This function will first go to a separate confirmation page, then delete
        //public ActionResult Delete(int id)
        //{
        //    if (!_repo.isExists(id))
        //    {
        //        return NotFound();
        //    }
        //    var leaveType = _repo.FindById(id);
        //    var model = _mapper.Map<DetailsLeaveTypeVM>(leaveType);             //Maps the ViewModel to the database class
        //    return View(model);
        //}

        //// POST: LeaveTypesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, DetailsLeaveTypeVM model)
        //{
        //    try
        //    {
        //        var leaveType = _repo.FindById(id);
        //        if (leaveType == null)
        //        {
        //            return NotFound();
        //        }
        //        var isSuccess = _repo.Delete(leaveType);

        //        if (!isSuccess)
        //        {
        //            ModelState.AddModelError("", "Delete was not successful");
        //            return View(model);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", $"Exception: {ex.Message}");
        //        return View(model);
        //    }
        //}
    }
}
