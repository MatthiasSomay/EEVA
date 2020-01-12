using EEVA.Domain.DataManager;
using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.models
{
    class CalculateHelper
    {
        private readonly StudentExamManager _studentExamManager;

        public CalculateHelper(EEVAContext context)
        {
            _studentExamManager = new StudentExamManager(context);
        }

        public int CalculatePoints(int id)
        {
            StudentExam studentExam = _studentExamManager.Get(id);
            int points = 0;
            foreach (StudentExamAnswer s in studentExam.StudentExamAnswers)
            {
                points += s.CalculatePoints();
            }
            return points;
        }

        public int TotalPoints(int id)
        {
            StudentExam studentExam = _studentExamManager.Get(id);
            return studentExam.Exam.ExamQuestions.Count;
        }
    }
}
