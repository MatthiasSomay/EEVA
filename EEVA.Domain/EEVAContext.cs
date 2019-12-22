using System;
using System.Collections.Generic;
using System.Text;
using EEVA.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EEVA.Domain
{
    public class EEVAContext : DbContext
    {
        public EEVAContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentExamAnswer> StudentExamAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
