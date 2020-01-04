using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
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
            return _eevaContext.Courses
                .Include(Course => Course.Questions)
                .Include(Course => Course.Exams)
                .Include(Course => Course.Exams).ThenInclude(Exam => Exam.Teacher)
                .FirstOrDefault(c => c.Id == id);
        } 

        public IEnumerable<Course> GetAll()
        {
            return _eevaContext.Courses
                .Include(Course => Course.Questions)
                .Include(Course => Course.Exams)
                .Include(Course => Course.Exams).ThenInclude(Exam => Exam.Teacher)
                .ToList();
        }

       

        public IEnumerable<Course> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            IEnumerable<Course> entities = _eevaContext.Courses
                .Include(Course => Course.Questions)
                .Include(Course => Course.Exams)
                .Include(Course => Course.Exams).ThenInclude(Exam => Exam.Teacher)
                .Where(c => c.CourseName.ToUpper()
                .Contains(keyword));

            return entities;
        }

        public void Update(Course entity)
        {
            Course dbEntity = Get(entity.Id);
            dbEntity.CourseName = entity.CourseName;
            dbEntity.CourseYear = entity.CourseYear;
            dbEntity.Questions = entity.Questions;
            dbEntity.Exams = entity.Exams;
            _eevaContext.SaveChanges();
        }
    }
}
