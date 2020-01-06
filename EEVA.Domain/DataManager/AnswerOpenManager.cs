using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class AnswerOpenManager : IDataManager<AnswerOpen>
    {
        public EEVAContext _eevaContext;

        public AnswerOpenManager()
        {

        }

        public AnswerOpenManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(AnswerOpen entity)
        {
            _eevaContext.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(AnswerOpen entity)
        {
            _eevaContext.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public AnswerOpen Get(int? id)
        {
            return _eevaContext.AnswersOpen.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<AnswerOpen> GetAll()
        {
            return _eevaContext.AnswersOpen.ToList();
        }

        public IEnumerable<AnswerOpen> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            return _eevaContext.AnswersOpen
                .Where(a => a.Keyword.ToUpper().Contains(keyword)).ToList();
        }

        public void Update(AnswerOpen entity)
        {
            AnswerOpen dbEntity = Get(entity.Id);
            dbEntity.Keyword = entity.Keyword;
            dbEntity.QuestionOpen = entity.QuestionOpen;
            _eevaContext.SaveChanges();
        }
    }
}
