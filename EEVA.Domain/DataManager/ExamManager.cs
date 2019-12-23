using EEVA.Domain.Models;
using EEVA.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class ExamManager : IDataManager<Exam>
    {
        private readonly EEVAContext _eevaContext;

        public ExamManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(Exam entity)
        {
            _eevaContext.Exams.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(Exam entity)
        {
            _eevaContext.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public Exam Get(int id)
        {
            return _eevaContext.Exams.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Exam> GetAll()
        {
            return _eevaContext.Exams.ToList();
        }

        public IEnumerable<Exam> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            List<Exam> exams = new List<Exam>();

            IEnumerable<Exam> entities = _eevaContext.Exams
                .Where(e => e.Course.CourseName.ToUpper().Contains(keyword) || e.Teacher.LastName.ToUpper().Contains(keyword) || e.Teacher.FirstName.ToUpper().Contains(keyword));
            foreach (var entity in entities)
            {
                exams.Add(new Exam()
                {
                    Id = entity.Id,
                    Course = entity.Course,
                    Date = entity.Date,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    ExamQuestions = entity.ExamQuestions,
                    StudentExams = entity.StudentExams

                });
            }
            return exams;
        }

        public void Update(Exam dbEntity, Exam entity)
        {
            dbEntity.Course = entity.Course;
            dbEntity.Date = entity.Date;
            dbEntity.StartTime = entity.StartTime;
            dbEntity.EndTime = entity.EndTime;
            dbEntity.ExamQuestions = entity.ExamQuestions;
            dbEntity.StudentExams = entity.StudentExams;

            _eevaContext.SaveChanges();
        }
    }
}
