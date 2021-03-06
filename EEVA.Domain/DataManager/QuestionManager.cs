﻿using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using Microsoft.EntityFrameworkCore;
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
            return _eevaContext.Questions
                .Include(Question => Question.Course)
                .Include(q => (q as QuestionMultipleChoice).Answers)
                .Include(q => (q as QuestionOpen).Answers)
                .FirstOrDefault(q => q.Id == id);
        }

        public QuestionMultipleChoice GetMultipleChoice(int? id)
        {
            return _eevaContext.Questions
                .Include(Question => Question.Course)
                .Include(q => (q as QuestionMultipleChoice).Answers)
                .OfType<QuestionMultipleChoice>()
                .FirstOrDefault(q => q.Id == id);
        }

        public QuestionOpen GetOpen(int? id)
        {
            return _eevaContext.Questions
                .Include(Question => Question.Course)
                .Include(q => (q as QuestionOpen).Answers)
                .OfType<QuestionOpen>()
                .FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Question> GetAll()
        {
            return _eevaContext.Questions
                .Include(Question => Question.Course)
                .ToList();
        }

        public IEnumerable<Question> Search(string keyword)
        {
            keyword = keyword.ToUpper();

            return _eevaContext.Questions
                 .Include(Question => Question.Course)
                .Where(q => q.QuestionPhrase.ToUpper().Contains(keyword) || q.Course.CourseName.ToUpper().Contains(keyword)).ToList();

        }

        public void Update(Question entity)
        {
            Question dbEntity = Get(entity.Id);
            dbEntity.QuestionPhrase = entity.QuestionPhrase;
            _eevaContext.SaveChanges();
        }
    }
}
