using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Course Name")]
        [Required]
        public string CourseName { get; set; }

        [Display(Name ="Course Year")]
        [Required]
        public string CourseYear { get; set; }

        public List<Question> Questions { get; set; }

        public List<Exam> Exams { get; set; }
    }
}
