using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
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
            return _eevaContext.StudentExams
                .Include(s => s.Exam).ThenInclude(e => e.ExamQuestions).ThenInclude(q => (q as QuestionMultipleChoice).Answers)
                .Include(s => s.Exam).ThenInclude(e => e.ExamQuestions).ThenInclude(q => (q as QuestionOpen).Answers)
                .Include(s => s.Exam).ThenInclude(e => e.Course)
                .Include(s => s.Student)
                .Include(s => s.StudentExamAnswers)
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<StudentExam> GetAll()
        {
            return _eevaContext.StudentExams
                .Include(s => s.Exam).ThenInclude(e => e.ExamQuestions)
                .Include(s => s.Exam).ThenInclude(e => e.Course)
                .Include(s => s.Student)
                .Include(s => s.StudentExamAnswers)
                .ToList();
        }

        public IEnumerable<StudentExam> GetAllByStudent(int id)
        {
            return _eevaContext.StudentExams
                .Include(s => s.Exam).ThenInclude(e => e.ExamQuestions)
                .Include(s => s.Exam).ThenInclude(e => e.Course)
                .Include(s => s.Student)
                .Include(s => s.StudentExamAnswers)
                .Where(s => s.Student.Id == id)
                .ToList();
        }

        public IEnumerable<StudentExam> Search(string keyword)
        {
            keyword = keyword.ToUpper();
            
            return _eevaContext.StudentExams
                .Where(s => s.Exam.Course.CourseName.ToUpper().Contains(keyword) 
                || s.Exam.Teacher.LastName.ToUpper().Contains(keyword) 
                || s.Student.LastName.ToUpper().Contains(keyword)).ToList();
        }

        public IEnumerable<StudentExam> SearchByStudent(string keyword, int id)
        {
            keyword = keyword.ToUpper();

            return _eevaContext.StudentExams
                .Where(s => s.Exam.Course.CourseName.ToUpper().Contains(keyword)
                || s.Exam.Teacher.LastName.ToUpper().Contains(keyword)
                || s.Student.LastName.ToUpper().Contains(keyword))
                .Where(s => s.Student.Id == id).ToList();
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
