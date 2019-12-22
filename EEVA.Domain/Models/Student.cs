using System;

namespace EEVA.Domain.Models
{
    public class Student : Contact
    {
        public Student(string firstName, string lastName, string email, string phone) : base(firstName, lastName, email, phone)
        {

        }

        public Student()
        {

        }
    }
}