using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Question Phrase")]
        [Required]
        public string QuestionPhrase { get; set; }

        [Display(Name = "Course")]
        [Required]
        public Course Course { get; set; }

        public List<ExamQuestion> Exams { get; set; }

        public Question()
        {
               
        }

        public Question(int id, string questionPhrase, Course course)
        {
            Id = id;
            QuestionPhrase = questionPhrase;
            Course = course;
        }

    }
}