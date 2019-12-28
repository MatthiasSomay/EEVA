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
        public IEnumerable<Question> ExamQuestions { get; set; }
        public IEnumerable<StudentExam> StudentExams { get; set; }

        //Used for Course dropdown in view
        public IEnumerable<Course> Courses { get; set; }
        public int CourseId { get; set; }

        //Used for Teacher dropdown in view
        public IEnumerable<Teacher> Teachers { get; set; }
        public int TeacherId { get; set; }

        public ExamViewModel(int id, Course course, Teacher teacher, DateTime date, TimeSpan startTime, TimeSpan endTime, IEnumerable<Question> examQuestions, IEnumerable<StudentExam> studentExams, IEnumerable<Course> courses, int courseId, IEnumerable<Teacher> teachers, int teacherId)
        {
            Id = id;
            Course = course;
            Teacher = teacher;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            ExamQuestions = examQuestions;
            StudentExams = studentExams;
            Courses = courses;
            CourseId = courseId;
            Teachers = teachers;
            TeacherId = teacherId;
        }

        public ExamViewModel(IEnumerable<Course> courses, IEnumerable<Teacher> teachers)
        {
            Courses = courses;
            Teachers = teachers;
        }

        public ExamViewModel()
        {
           
        }
    }
}
