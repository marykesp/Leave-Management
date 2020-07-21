using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Management.Models
{
    public class EmployeeVM
    {
        public string Id { get; set; }                      //The first 3 are coming from the AspNetUsers table.  Only include the necessary 
        
        [DisplayName("User Name")]
        public string UserName { get; set; }                //fields in the ViewModel.  The field names must match the database names
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Tax ID")]
        public string TaxId { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Date Joined")]
        public DateTime DateJoined { get; set; }
    }
}
