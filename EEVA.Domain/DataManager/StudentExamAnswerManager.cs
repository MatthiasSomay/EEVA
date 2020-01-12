using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class StudentExamAnswerManager : IDataManager<StudentExamAnswer>
    {
        private readonly EEVAContext _eevaContext;
        public StudentExamAnswerManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(StudentExamAnswer entity)
        {
            _eevaContext.StudentExamAnswers.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(StudentExamAnswer entity)
        {
            _eevaContext.StudentExamAnswers.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public StudentExamAnswer Get(int? id)
        {
            return _eevaContext.StudentExamAnswers
                .Include(s => s.StudentExam)
                .Include(s => (s as StudentExamAnswerMultipleChoice).Question).ThenInclude(q => q.Answers)
                .Include(s => (s as StudentExamAnswerOpen).Question).ThenInclude(q => q.Answers)
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<StudentExamAnswer> GetAll()
        {
            return _eevaContext.StudentExamAnswers.ToList();
        }

        public IEnumerable<StudentExamAnswer> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public void UpdateOpen(StudentExamAnswerOpen entity, StudentExamAnswerOpen dbEntity)
        {
            dbEntity.Question = entity.Question;
            dbEntity.StudentExam = entity.StudentExam;
            dbEntity.Answer = entity.Answer;
            _eevaContext.SaveChanges();
        }

        public void UpdateMultiple(StudentExamAnswerMultipleChoice entity, StudentExamAnswerMultipleChoice dbEntity)
        {
            dbEntity.Question = entity.Question;
            dbEntity.StudentExam = entity.StudentExam;
            dbEntity.Answer = entity.Answer;
            _eevaContext.SaveChanges();
        }

        public StudentExamAnswerOpen GetByStudentExamAndQuestionOpen(int studentExamId, int questionId)
        {
            return _eevaContext.StudentExamAnswers
                .OfType<StudentExamAnswerOpen>()
                .Where(a => a.StudentExam.Id == studentExamId && a.Question.Id == questionId)
                .FirstOrDefault();
        }

        public StudentExamAnswerMultipleChoice GetByStudentExamAndQuestionMultiple(int studentExamId, int questionId)
        {
            return _eevaContext.StudentExamAnswers
                .OfType<StudentExamAnswerMultipleChoice>()
                .Where(a => a.StudentExam.Id == studentExamId && a.Question.Id == questionId)
                .FirstOrDefault();
        }

        public void Update(StudentExamAnswer entity)
        {
            throw new NotImplementedException();
        }
    }
}
