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
        private readonly EEVAContext _eevaContext;

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

        public IEnumerable<Answer> GetAll()
        {
            return _eevaContext.Answers.ToList();
        }

        public IEnumerable<Answer> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            List<Answer> answers = new List<Answer>();

            IEnumerable<Answer> entities = _eevaContext.Answers
                .Where(a => a.Question.QuestionPhrase.ToUpper().Contains(keyword));
            foreach (var entity in entities)
            {
                answers.Add(new Answer()
                {
                    Id = entity.Id,
                    Question = entity.Question
                });
            }
            return answers;
        }

        public void Update(Answer entity)
        {
            Answer dbEntity = Get(entity.Id);
            dbEntity.Question = entity.Question;
            _eevaContext.SaveChanges();
        }
    }
}
