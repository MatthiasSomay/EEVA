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
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentExamAnswer> StudentExamAnswers { get; set; }
        public DbSet<AnswerMultipleChoice> AnswerMultipleChoices { get; set; }
        public DbSet<AnswerOpen> AnswerOpens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>();
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<Exam>();
            modelBuilder.Entity<StudentExam>();
            modelBuilder.Entity<Question>();
            modelBuilder.Entity<Answer>();
            modelBuilder.Entity<StudentExamAnswer>();
        }
    }
}
