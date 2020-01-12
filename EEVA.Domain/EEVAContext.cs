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
        public DbSet<StudentExamAnswerOpen> StudentExamAnswersOpen { get; set; }
        public DbSet<StudentExamAnswerMultipleChoice> StudentExamAnswersMultipleChoice { get; set; }
        public DbSet<AnswerMultipleChoice> AnswersMultipleChoice { get; set; }
        public DbSet<AnswerOpen> AnswersOpen { get; set; }
        public DbSet<QuestionMultipleChoice> QuestionsMultipleChoice { get; set; }
        public DbSet<QuestionOpen> QuestionsOpen { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Contact>();
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<Exam>();
            modelBuilder.Entity<StudentExam>();
            modelBuilder.Entity<Question>();
            modelBuilder.Entity<AnswerMultipleChoice>();
            modelBuilder.Entity<AnswerOpen>();
            modelBuilder.Entity<StudentExamAnswer>();

            modelBuilder.Entity<ExamStudent>()
                .HasKey(t => new { t.StudentId, t.ExamId });

            modelBuilder.Entity<ExamStudent>()
                .HasOne(e => e.Exam)
                .WithMany(s => s.Students)
                .HasForeignKey(e => e.ExamId);

            modelBuilder.Entity<ExamStudent>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<ExamQuestion>()
              .HasKey(t => new { t.ExamId, t.QuestionId });

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(e => e.Question)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.QuestionId);

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(e => e.Exam)
                .WithMany(s => s.Questions)
                .HasForeignKey(e => e.ExamId);




        }

       
    }
    public class ExamStudent
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public ExamStudent(Exam e, Student s)
        {
            ExamId = e.Id;
            Exam = e;
            StudentId = s.Id;
            Student = s;
        }
        public ExamStudent()
        {

        }


    }

    public class ExamQuestion
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }


    }


}
