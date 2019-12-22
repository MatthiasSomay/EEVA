using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Course Course { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<Question> ExamQuestions { get; set; }
        public List<StudentExam> StudentExams { get; set; }

        public Exam(Course course, DateTime date, TimeSpan start, TimeSpan end, List<Question> questions, List<StudentExam> studentExams)
        {
            this.Course = course;
            this.Date = date;
            this.StartTime = start;
            this.EndTime = end;
            this.ExamQuestions = questions;
            this.StudentExams = studentExams;
        }
    }
}
