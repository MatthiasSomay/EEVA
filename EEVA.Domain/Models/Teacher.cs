using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class Teacher : Contact
    {
        public Teacher(int id, string firstName, string lastName, string email, string phone) : base(id, firstName, lastName, email, phone)
        {
        }

        public Teacher()
        {

        }
    }
}
