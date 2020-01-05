using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class AnswerManager : IDataManager<Answer>
    {
        public EEVAContext _eevaContext;

        public AnswerManager()
        {

        }

        public AnswerManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(Answer entity)
        {
            _eevaContext.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(Answer entity)
        {
            _eevaContext.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public Answer Get(int? id)
        {
            return _eevaContext.Answers.FirstOrDefault(a => a.Id == id);
        }

        public AnswerOpen GetOpen(int? id)
        {
            return _eevaContext.Answers
                .Include(a => (a as AnswerOpen).Keyword)
                .OfType<AnswerOpen>()
                .FirstOrDefault(q => q.Id == id);
        }

        public AnswerMultipleChoice GetMultipleChoice(int? id)
        {
            return _eevaContext.Answers
                .Include(a => (a as AnswerMultipleChoice).Answer)
                .Include(a => (a as AnswerMultipleChoice).IsAnswerCorrect)
                .OfType<AnswerMultipleChoice>()
                .FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Answer> GetAll()
        {
            return _eevaContext.Answers.ToList();
        }

        public IEnumerable<Answer> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            return _eevaContext.Answers
                .Where(a => a.Question.QuestionPhrase.ToUpper().Contains(keyword)).ToList();
        }

        public void Update(Answer entity)
        {
            Answer dbEntity = Get(entity.Id);
            dbEntity.Question = entity.Question;
            _eevaContext.SaveChanges();
        }
    }
}
