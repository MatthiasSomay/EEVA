using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class StudentViewModel
    {

        public int Id { get; set; }
        [Display(Name = " First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public StudentViewModel(int id, string firstName, string lastName, string fullName, string email, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public StudentViewModel()
        {

        }
    }
}
