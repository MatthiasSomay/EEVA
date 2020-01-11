using EEVA.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Web.Models
{
    public class CourseViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Course Name")]
        [Required]
        public string CourseName { get; set; }

        [Display(Name = "Course Year")]
        [Required]
        public string CourseYear { get; set; }

        public List<Question> Questions { get; set; }

        public List<Exam> Exams { get; set; }

        public CourseViewModel()
        {
            
        }

        public CourseViewModel(int id, string courseName, string courseYear, List<Question> questions, List<Exam> exams)
        {
            Id = id;
            CourseName = courseName;
            CourseYear = courseYear;
            Questions = questions;
            Exams = exams;
        }
    }
}
