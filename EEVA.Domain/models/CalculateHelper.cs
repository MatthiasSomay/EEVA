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

        public void CalculatePoints(int id)
        {
            StudentExam studentExam = _studentExamManager.Get(id);
            double points = 0;
            foreach (StudentExamAnswer s in studentExam.StudentExamAnswers)
            {
                points += s.CalculatePoints();
            }
            studentExam.Points = points;
            _studentExamManager.Update(studentExam);
        }

        public void TotalPoints(int id)
        {
            StudentExam studentExam = _studentExamManager.Get(id);
            studentExam.OnPoints = studentExam.Exam.ExamQuestions.Count;
            _studentExamManager.Update(studentExam);
        }
    }
}
