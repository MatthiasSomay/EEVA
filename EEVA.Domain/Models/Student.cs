using System;

namespace EEVA.Domain.Models
{
    public class Student : Contact
    {
        public Student(int id, string firstName, string lastName, string email, string phone) : base(id, firstName, lastName, email, phone)
        {

        }

        public Student()
        {

        }
    }
}