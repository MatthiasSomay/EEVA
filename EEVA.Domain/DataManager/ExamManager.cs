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
            return _eevaContext.Exams
                .Include(Exam => Exam.Teacher)
                .Include(Exam => Exam.Course)
                .Include(Exam => Exam.StudentExams)
                .Include(Exam => Exam.ExamQuestions)
                .ThenInclude(Question => (Question as QuestionMultipleChoice).Answers)
                .FirstOrDefault(e => e.Id == id);
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

        public IEnumerable<Exam> GetExamssOfTeacher(int teacherId)
        {

            return _eevaContext.Exams
               .Include(Exam => Exam.Teacher)
               .Include(Exam => Exam.Course)
               .Where(E => E.Teacher.Id == teacherId).ToList();
           

        }

        public IEnumerable<Exam> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            return _eevaContext.Exams
                .Include(Exam => Exam.Teacher)
                .Include(Exam => Exam.Course)
                .Include(Exam => Exam.StudentExams)
                .Include(Exam => Exam.ExamQuestions)
                .Where(e => e.Course.CourseName.ToUpper()
                .Contains(keyword) || e.Teacher.LastName.ToUpper()
                .Contains(keyword) || e.Teacher.FirstName.ToUpper()
                .Contains(keyword))
                .ToList();
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
