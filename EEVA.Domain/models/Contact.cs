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

        public Contact(string firstName, string lastName, string email, string phone)
        {
           
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phone;
        }

        public Contact()
        {
        }
    }
}
