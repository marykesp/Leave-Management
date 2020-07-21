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
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class CreateLeaveTypeVM
    {
        [Required(ErrorMessage = "Please enter the Leave Type")]
        [MaxLength(50, ErrorMessage = "The Leave Type Name cannot exceed 50 characters")]
        [DisplayName("Leave Type")]
        public string Name { get; set; }        
    }
}
