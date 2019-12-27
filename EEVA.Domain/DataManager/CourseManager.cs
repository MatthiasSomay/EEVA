using EEVA.Domain.Models;
using EEVA.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
   public class CourseManager : IDataManager<Course>
    {
        public EEVAContext _eevaContext { get; set; }

        public CourseManager()
        {
        }

        public CourseManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(Course entity)
        {
            _eevaContext.Courses.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(Course entity)
        {
            _eevaContext.Courses.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public Course Get(int? id)
        {
            return _eevaContext.Courses.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Course> GetAll()
        {
            return _eevaContext.Courses.ToList();
        }

        public IEnumerable<Course> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            List<Course> courses = new List<Course>();

            IEnumerable<Course> entities = _eevaContext.Courses
                .Where(c => c.CourseName.ToUpper().Contains(keyword));
            foreach (var entity in entities)
            {
                courses.Add(new Course()
                {
                    Id = entity.Id,
                    CourseName = entity.CourseName,
                    CourseYear = entity.CourseYear,
                    Exams = entity.Exams,
                    Questions = entity.Questions
                });
            }
            return courses;
        }

        public void Update(Course dbEntity, Course entity)
        {
            dbEntity.CourseName = entity.CourseName;
            dbEntity.CourseYear = entity.CourseYear;
            dbEntity.Questions = entity.Questions;
            dbEntity.Exams = entity.Exams;
            _eevaContext.SaveChanges();
        }
    }
}
