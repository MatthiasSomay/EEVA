using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class CourseViewModel
    {
       
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseYear { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Exam> Exams { get; set; }
    }
}
