using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
