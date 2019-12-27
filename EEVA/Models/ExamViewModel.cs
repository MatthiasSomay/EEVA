using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class ExamViewModel
    {
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

        //Used for Teacher dropdown in view
        public IEnumerable<Teacher> Teachers { get; set; }

        //Used for selected value in the Course dropdown
        public int SelectedValueCourse()
        {
            if (Course == null)
            {
                return 0;
            }
            else return Course.Id;
        }

        //Used for selected value in the Teacher dropdown
        public int SelectedValueTeacher()
        {
            if (Teacher == null)
            {
                return 0;
            }
            else return Teacher.Id;
        }
    }
}
