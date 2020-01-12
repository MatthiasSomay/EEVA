using System;
using System.Collections.Generic;
using static EEVA.Domain.EEVAContext;

namespace EEVA.Domain.Models
{
    public class Student : Contact
    {
       
        public List<ExamStudent> Exams { get; set; }
        public Student(int id, string firstName, string lastName, string email, string phone) : base(id, firstName, lastName, email, phone)
        {

        }

        public Student()
        {

        }
    }
}