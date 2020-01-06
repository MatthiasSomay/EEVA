using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class QuestionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Question Phrase")]
        [Required]
        public string QuestionPhrase { get; set; }

        [Display(Name = "Course")]
        public Course Course { get; set; }

        //Used for Course dropdown in view
        public IEnumerable<Course> Courses { get; set; }
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public QuestionViewModel(int id, string questionPhrase, Course course, IEnumerable<Course> courses)
        {
            Id = id;
            QuestionPhrase = questionPhrase;
            Course = course;
            if (Course != null)
            {
                CourseName = course.CourseName;
                CourseId = course.Id;
            }
            Courses = courses;
        }
        public QuestionViewModel()
        {
            
        }

        public QuestionViewModel(IEnumerable<Course> courses)
        {
            Courses = courses;
        }
    }
}
