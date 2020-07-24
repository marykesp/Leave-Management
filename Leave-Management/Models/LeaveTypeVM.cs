using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Models
{
    public class DetailsLeaveTypeVM
    {
        [DisplayName("Leave Type ID")]
        public int Id { get; set; }

        [DisplayName("Leave Type")]
        public string Name { get; set; }

        [DisplayName("Default Allocation")]
        public int DefaultDays { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
    }
    public class CreateLeaveTypeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Leave Type")]
        [MaxLength(50, ErrorMessage = "The Leave Type Name cannot exceed 50 characters")]
        [DisplayName("Leave Type")]
        public string Name { get; set; }

        [DisplayName("Default Allocation per Year")]
        [Required(ErrorMessage = "Please capture the default number of days for this Leave Type")]
        [Range(1,25,ErrorMessage = "Number must be between 1 and 25")]
        public int DefaultDays { get; set; }

        [DisplayName("Date Created")]        
        public DateTime? DateCreated { get; set; }          //The question mark ensures that the user is not forced to capture this value
    }
}
