using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String Lastname { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
    }
}
