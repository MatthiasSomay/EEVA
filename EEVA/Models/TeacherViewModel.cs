using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class TeacherViewModel
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
        public IEnumerable<Exam> Exams { get; set; }

        public TeacherViewModel(int id, string firstName, string lastName, string fullName, string email, string phoneNumber, IEnumerable<Exam> exams)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            Email = email;
            Exams = exams;
           
        }

        public TeacherViewModel()
        {
        }
    }
}
