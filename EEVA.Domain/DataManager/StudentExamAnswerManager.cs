using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
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
            return _eevaContext.StudentExamAnswers.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<StudentExamAnswer> GetAll()
        {
            return _eevaContext.StudentExamAnswers.ToList();
        }

        public IEnumerable<StudentExamAnswer> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(StudentExamAnswer entity, StudentExamAnswer dbEntity)
        {
            dbEntity.Question = entity.Question;
            dbEntity.StudentExam = entity.StudentExam;
            _eevaContext.SaveChanges();
        }

        public StudentExamAnswerOpen GetByStudentExamAndQuestionOpen(int studentExamId, int questionId)
        {
            return _eevaContext.StudentExamAnswers
                .Where(a => a.StudentExam.Id == studentExamId && a.Question.Id == questionId)
                .OfType<StudentExamAnswerOpen>()
                .FirstOrDefault();
        }

        public StudentExamAnswerMultipleChoice GetByStudentExamAndQuestionMultiple(int studentExamId, int questionId)
        {
            return _eevaContext.StudentExamAnswers
                .Where(a => a.StudentExam.Id == studentExamId && a.Question.Id == questionId)
                .OfType<StudentExamAnswerMultipleChoice>()
                .FirstOrDefault();
        }
    }
}
