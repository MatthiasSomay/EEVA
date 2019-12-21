using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class ContactViewModel
    {
        public string Subtitle
        {
            get { return $"Details van {FirstName} {LastName}"; }
        }

        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
    }
    }

