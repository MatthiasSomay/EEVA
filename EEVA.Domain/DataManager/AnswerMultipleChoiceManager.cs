using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class AnswerMultipleChoiceManager : IDataManager<AnswerMultipleChoice>
    {
        public EEVAContext _eevaContext;

        public AnswerMultipleChoiceManager()
        {

        }

        public AnswerMultipleChoiceManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(AnswerMultipleChoice entity)
        {
            _eevaContext.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(AnswerMultipleChoice entity)
        {
            _eevaContext.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public AnswerMultipleChoice Get(int? id)
        {
            return _eevaContext.AnswersMultipleChoice.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<AnswerMultipleChoice> GetAll()
        {
            return _eevaContext.AnswersMultipleChoice.ToList();
        }

        public IEnumerable<AnswerMultipleChoice> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            return _eevaContext.AnswersMultipleChoice
                .Where(a => a.Answer.ToUpper().Contains(keyword)).ToList();
        }

        public void Update(AnswerMultipleChoice entity)
        {
            AnswerMultipleChoice dbEntity = Get(entity.Id);
            dbEntity.Answer = entity.Answer;
            dbEntity.IsAnswerCorrect = entity.IsAnswerCorrect;
            dbEntity.QuestionMultipleChoice = entity.QuestionMultipleChoice;
            _eevaContext.SaveChanges();
        }
    }
}
