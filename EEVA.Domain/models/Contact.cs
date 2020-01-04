using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required] 
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phonenumber")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Name")]
        [NotMapped]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }

        public Contact(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
        }

        public Contact()
        {
        }

    }
}
