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
    public class CourseManagerTest : IDisposable
    {

        private readonly CourseManager _manager = new CourseManager();
        private Course COURSE = new Course { CourseName = "ASP .NET", CourseYear = "2019-2020", Questions = null, Exams = null };

        public CourseManagerTest()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EEVAContext>();

            builder.UseSqlServer($"Server=tcp:eevakbms.database.windows.net,1433;Database = courses_db_{ Guid.NewGuid()};Persist Security Info=False;User ID=eevakbms;Password=EEVA_KBMS007;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                    .UseInternalServiceProvider(serviceProvider);

            _manager._eevaContext = new EEVAContext(builder.Options);
            _manager._eevaContext.Database.Migrate();
        }

        [Fact]
        public void AddCourse_Test()
        {
            _manager.Add(COURSE);

            IList<Course> courses = _manager.GetAll().ToList();

            Assert.Equal(1, courses.Count);
            Course course = courses.First();
            Assert.Equal("ASP .NET", course.CourseName);
        }

        [Fact]
        public void DeleteCourse_Test()
        {
            _manager.Add(COURSE);

            IList<Course> courses = _manager.GetAll().ToList();
            Course course = courses.First();

            _manager.Delete(course);

            IList<Course> deletedCourses = _manager.GetAll().ToList();

            Assert.Equal(0, deletedCourses.Count);

        }

        [Fact]
        public void GetCourse_Test()
        {
            _manager.Add(COURSE);

            Course course = _manager.Get(1);

            Assert.Equal("ASP .NET", course.CourseName);
        }

        [Fact]
        public void GetAllCourses_Test()
        {
            _manager.Add(COURSE);
            _manager.Add(new Course { CourseName = "Trends in ICT", CourseYear = "2019-2020", Questions = null, Exams = null });

            IList<Course> courses = _manager.GetAll().ToList();

            Assert.Equal(2, courses.Count);

        }

        [Fact]
        public void SearchCourses_Test()
        {
            _manager.Add(COURSE);

            IList<Course> courses = _manager.Search("asp").ToList();

            Assert.True(courses.Count > 0);

        }

        [Fact]
        public void UpdateCourse_Test()
        {
            _manager.Add(COURSE);

            IList<Course> courses = _manager.GetAll().ToList();
            Course course = courses.First();
            course.CourseName = "Trends in ICT";

            _manager.Update(course);

            IList<Course> updatedCourses = _manager.GetAll().ToList();
            Course updatedCourse = updatedCourses.First();

            Assert.Equal("Trends in ICT", updatedCourse.CourseName);

        }


        public void Dispose()
        {
            _manager._eevaContext.Database.EnsureDeleted();
        }
    }
}
