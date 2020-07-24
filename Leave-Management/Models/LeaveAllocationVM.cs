
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Models
{
    public class DetailLeaveAllocationVM
    {
        [DisplayName("Leave Allocation ID")]
        public int Id { get; set; }

        [DisplayName("Number of Days")]
        public int NumberOfDays { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }        
        public EmployeeVM Employee { get; set; }      //NB:  You don't add a reference to the Data.Models tables here.  This layer must not know about the DB
        public string EmployeeId { get; set; }      //The Data Type of the Id in the AspNetUsers table is string for a GUID   

        [DisplayName("Leave Types")]
        public DetailsLeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [DisplayName("Leave Period")]
        public int LeavePeriod { get; set; }

    }

    public class CreateLeaveAllocationVM
    {
        public int Id { get; set; }

        [DisplayName("Number of Days")]        
        public int NumberOfDays { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        
        public EmployeeVM Employee { get; set; }    //NB:  You don't add a reference to the Data.Models tables here.  This layer must not know about the DB
        public string EmployeeId { get; set; }      //The Data Type of the Id in the AspNetUsers table is string for a GUID


        [DisplayName("Leave Type")]
        public DetailsLeaveTypeVM LeaveType { get; set; }  //NB:  You don't add a reference to the Data.Models tables here.  This layer must not know about the DB
        public int LeaveTypeId { get; set; }
        
        [DisplayName("Leave Period")]
        [Required]
        public int LeavePeriod { get; set; }
    }

    public class SetLeaveAllocationVM 
    {
        public int NumberUpdated { get; set; }
        public List<DetailsLeaveTypeVM> LeaveTypes { get; set; }
    }

    public class ViewAllocationsVM
    {
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<DetailLeaveAllocationVM> LeaveAllocations { get; set; }

    }

    public class EditEmployeeAllocationVM
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public EmployeeVM Employee { get; set; }

        [Required(ErrorMessage = "Please enter the new Number of Days")]
        [DisplayName("Number of Days")]
        public int NumberOfDays { get; set; }
        public DetailsLeaveTypeVM LeaveType { get; set; }
        
    }
}
