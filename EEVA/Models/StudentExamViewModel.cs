using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class StudentExamViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Student Student { get; set; }
        [Required]
        public Exam Exam { get; set; }
        public IEnumerable<StudentExamAnswer> StudentExamAnswers { get; set; }

        public int Counter { get; set; }

        [Display(Name = "Answer")]
        public string StudentExamAnswer { get; set; }

        [Display(Name = "Question")]
        public Question Question { get; set; }

        public StudentExamViewModel(int id, Student student, Exam exam, IEnumerable<StudentExamAnswer> studentExamAnswers)
        {
            Id = id;
            Student = student;
            Exam = exam;
            StudentExamAnswers = studentExamAnswers;
           
        }

        public StudentExamViewModel() { }

        public StudentExamViewModel(Student student, Exam exam)
        {
            Student = student;
            Exam = exam;
        }
    }
}
