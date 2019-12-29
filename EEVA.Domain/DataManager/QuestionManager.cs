﻿using EEVA.Domain.Models;
using EEVA.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEVA.Domain.DataManager
{
    public class QuestionManager : IDataManager<Question>
    {
        public EEVAContext _eevaContext;

        public QuestionManager()
        {

        }
        public QuestionManager(EEVAContext context)
        {
            _eevaContext = context;
        }
        public void Add(Question entity)
        {
            _eevaContext.Questions.Add(entity);
            _eevaContext.SaveChanges();
        }

        public void Delete(Question entity)
        {
            _eevaContext.Questions.Remove(entity);
            _eevaContext.SaveChanges();
        }

        public Question Get(int? id)
        {
            return _eevaContext.Questions.FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Question> GetAll()
        {
            //testing remove
            IEnumerable<Question> l = _eevaContext.Questions.ToList();
            return _eevaContext.Questions.ToList();
        }

        public IEnumerable<Question> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            List<Question> questions = new List<Question>();

            IEnumerable<Question> entities = _eevaContext.Questions
                .Where(q => q.QuestionPhrase.ToUpper().Contains(keyword));
            foreach (var entity in entities)
            {
                questions.Add(new Question()
                {
                    Id = entity.Id,
                    QuestionPhrase = entity.QuestionPhrase
                });
            }
            return questions;
        }

        public void Update(Question entity)
        {
            Question dbEntity = Get(entity.Id);
            dbEntity.QuestionPhrase = entity.QuestionPhrase;
            _eevaContext.SaveChanges();
        }
    }
}
