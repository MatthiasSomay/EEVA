using System;
using System.Collections.Generic;
using System.Text;
using EEVA.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace EEVA.Domain
{
    public class EEVAContext : DbContext
    {
        public EEVAContext()
        {
        }

        public EEVAContext(DbContextOptions<EEVAContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentExamAnswer> StudentExamAnswers { get; set; }
        public DbSet<AnswerMultipleChoice> AnswersMultipleChoice { get; set; }
        public DbSet<AnswerOpen> AnswersOpen { get; set; }
        public DbSet<QuestionMultipleChoice> QuestionsMultipleChoice { get; set; }
        public DbSet<QuestionOpen> QuestionsOpen { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Course course = new Course() { Id = 1, CourseName = "ASP.NET", CourseYear = "2019-2020", Exams = null, Questions = null };
            Teacher teacher = new Teacher() { Id = 1, FirstName = "Kenneth", LastName = "Van Den Borne", Email = "kenneth@live.be", PhoneNumber = "046729292" };
            Exam exam = new Exam() { Id = 1, Course = course, Date = DateTime.Today, EndTime = TimeSpan.FromHours(12), StartTime = TimeSpan.FromHours(9), };




            modelBuilder.Entity<Contact>();
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<Exam>();
            modelBuilder.Entity<StudentExam>();
            modelBuilder.Entity<Question>();
            modelBuilder.Entity<AnswerMultipleChoice>();
            modelBuilder.Entity<AnswerOpen>();
            modelBuilder.Entity<StudentExamAnswer>();

            
        }
    }
}
