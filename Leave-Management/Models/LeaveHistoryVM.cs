
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Models
{
    public class DetailLeaveHistoryVM
    {
        [DisplayName("Leave History ID")]
        public int Id { get; set; }

        public EmployeeVM RequestingEmployee { get; set; }  //NB:  You don't add a reference to the Data.Models tables here.  This layer must not know about the DB
        public string RequestingEmployeeId { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public DetailsLeaveTypeVM LeaveType { get; set; }  //NB:  You don't add a reference to the Data.Models tables here.  This layer must not know about the DB
        public int LeaveTypeId { get; set; }

        [DisplayName("Date Requested")]
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }

        public EmployeeVM ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }

    public class CreateLeaveHistoryVM
    {
       
        //[Required(ErrorMessage = "Please select the employee requesting the leave")]
        public EmployeeVM RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }

        [Required(ErrorMessage = "Please select the Leave Start Date")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "Please select the Leave End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage ="Please select the Leave Type")]
        public DetailsLeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [DisplayName("Leave Types")]
        public IEnumerable<SelectListItem> LeaveTypes  { get; set; }

        [DisplayName("Request Date")]
        public DateTime DateRequested { get; set; }
        
        //public DateTime DateActioned { get; set; }
        //public bool? Approved { get; set; }
        //public Employee ApprovedBy { get; set; }
        //public string ApprovedById { get; set; }
    }
}
