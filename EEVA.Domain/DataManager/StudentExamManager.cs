using EEVA.Domain.Models;
using EEVA.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class StudentExamManager : IDataManager<StudentExam>
    {
        private readonly EEVAContext _eevaContext;
        public StudentExamManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(StudentExam entity)
        {
            _eevaContext.StudentExams.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(StudentExam entity)
        {
            _eevaContext.StudentExams.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public StudentExam Get(int? id)
        {
            return _eevaContext.StudentExams.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<StudentExam> GetAll()
        {
            return _eevaContext.StudentExams.ToList();
        }

        public IEnumerable<StudentExam> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            List<StudentExam> studentExams = new List<StudentExam>();

            IEnumerable<StudentExam> entities = _eevaContext.StudentExams
                .Where(s => s.Exam.Course.CourseName.ToUpper().Contains(keyword) || s.Exam.Teacher.LastName.ToUpper().Contains(keyword) || s.Student.LastName.ToUpper().Contains(keyword));
            foreach (var entity in entities)
            {
                studentExams.Add(new StudentExam()
                {
                    Id = entity.Id,
                    Exam = entity.Exam,
                    Student = entity.Student,
                    StudentExamAnswers = entity.StudentExamAnswers
                });
            }
            return studentExams;
        }

        public void Update(StudentExam entity)
        {
            StudentExam dbEntity = Get(entity.Id);
            dbEntity.Exam = entity.Exam;
            dbEntity.Student = entity.Student;
            dbEntity.StudentExamAnswers = entity.StudentExamAnswers;
            _eevaContext.SaveChanges();
        }
    }
}
