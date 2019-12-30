using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EEVA.Domain.DataManager
{
    public class ExamManager : IDataManager<Exam>
    {
        public EEVAContext _eevaContext;
        public ExamManager()
        {

        }
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

        public Exam Get(int? id)
        {
            Exam exam = _eevaContext.Exams
                .Include(Exam => Exam.Teacher)
                .Include(Exam => Exam.Course)
                .Include(Exam => Exam.StudentExams)
                .Include(Exam => Exam.ExamQuestions)
                .FirstOrDefault(e => e.Id == id);
            return exam;
        }

        public IEnumerable<Exam> GetAll()
        {
            return _eevaContext.Exams
                .Include(Exam => Exam.Teacher)
                .Include(Exam => Exam.Course)
                .Include(Exam => Exam.StudentExams)
                .Include(Exam => Exam.ExamQuestions)
                .ToList();
        }

        public IEnumerable<Exam> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            IEnumerable<Exam> entities = _eevaContext.Exams
                .Include(Exam => Exam.Teacher)
                .Include(Exam => Exam.Course)
                .Include(Exam => Exam.StudentExams)
                .Include(Exam => Exam.ExamQuestions)
                .Where(e => e.Course.CourseName.ToUpper()
                .Contains(keyword) || e.Teacher.LastName.ToUpper()
                .Contains(keyword) || e.Teacher.FirstName.ToUpper()
                .Contains(keyword));

            return new List<Exam>(entities);
        }

        public void Update(Exam entity)
        {
            Exam dbEntity = Get(entity.Id);

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
