using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class Teacher : Contact
    {
        public Teacher(string firstName, string lastName, string email, string phone) : base(firstName, lastName, email, phone)
        {
        }
    }
}
