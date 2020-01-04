using EEVA.Domain;
using EEVA.Domain.Models;
using EEVA.Domain.DataManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace EEVA.xUnitTest
{
    public class ExamManagerTest : IDisposable
    {
        private readonly ExamManager _manager = new ExamManager();
        private Exam EXAM = new Exam
        {
            Course = new Course { CourseName = "ASP .NET", CourseYear = "2019-2020", Questions = null, Exams = null },
            Teacher = new Teacher { FirstName = "Kenneth", LastName = "Van Den Borne", Email = "kenneth@gmail.com", PhoneNumber = "03893843949" },
            Date = new DateTime(2020, 1, 15),
            StartTime = new TimeSpan(15, 00, 00),
            EndTime = new TimeSpan(18, 00, 00),
            ExamQuestions = null,
            StudentExams = null
        }; 

        public ExamManagerTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EEVAContext>();

            builder.UseSqlServer($"Server=tcp:eevakbms.database.windows.net,1433;Database = exams_db_{ Guid.NewGuid()};Persist Security Info=False;User ID=eevakbms;Password=EEVA_KBMS007;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                    .UseInternalServiceProvider(serviceProvider);

            _manager._eevaContext = new EEVAContext(builder.Options);
            _manager._eevaContext.Database.Migrate();
        }

        [Fact]
        public void AddExam_Test()
        {
            _manager.Add(EXAM);

            IList<Exam> exams = _manager.GetAll().ToList();

            Assert.Equal(1, exams.Count);
            Exam exam = exams.First();
            Assert.Equal("ASP .NET", exam.Course.CourseName);
        }

        [Fact]
        public void DeleteExam_Test()
        {
            _manager.Add(EXAM);

            IList<Exam> exams = _manager.GetAll().ToList();
            Exam exam = exams.First();

            _manager.Delete(exam);

            IList<Exam> deletedExams = _manager.GetAll().ToList();

            Assert.Equal(0, deletedExams.Count);

        }

        [Fact]
        public void GetExam_Test()
        {
            _manager.Add(EXAM);

            Exam exam = _manager.Get(1);

            Assert.Equal("ASP .NET", exam.Course.CourseName);
        }

        [Fact]
        public void GetAllExams_Test()
        {
            _manager.Add(EXAM);
            _manager.Add(new Exam
            {
                Course = new Course { CourseName = "Trends in ICT", CourseYear = "2019-2020", Questions = null, Exams = null },
                Teacher = new Teacher { FirstName = "Eric", LastName = "Michiels", Email = "e.michiels@gmail.com", PhoneNumber = "067272829" },
                Date = new DateTime(2020, 1, 20),
                StartTime = new TimeSpan(15, 00, 00),
                EndTime = new TimeSpan(18, 00, 00),
                ExamQuestions = null,
                StudentExams = null
            });

            IList<Exam> exams = _manager.GetAll().ToList();

            Assert.Equal(2, exams.Count);

        }

        [Fact]
        public void SearchExams_Test()
        {
            _manager.Add(EXAM);

            IList<Exam> exams = _manager.Search("asp").ToList();

            Assert.True(exams.Count > 0);

        }

        [Fact]
        public void UpdateCourse_Test()
        {
            _manager.Add(EXAM);

            IList<Exam> exams = _manager.GetAll().ToList();
            Exam exam = exams.First();
            exam.Course.CourseName = "Trends in ICT";

            _manager.Update(exam);

            IList<Exam> updatedExams = _manager.GetAll().ToList();
            Exam updatedExam = updatedExams.First();

            Assert.Equal("Trends in ICT", updatedExam.Course.CourseName);

        }

        public void Dispose()
        {
            _manager._eevaContext.Database.EnsureDeleted();

        }
    }
}
