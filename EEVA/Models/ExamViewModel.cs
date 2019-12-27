using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class ExamViewModel
    {
        public string Subtitle
        {
            get { return $"Details van examen {Course.CourseName} - {Date} '"; }
        }
        public int Id { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<Question> ExamQuestions { get; set; }
        public List<StudentExam> StudentExams { get; set; }
    }
}
