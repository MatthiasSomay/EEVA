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

        [Display(Name = "Score")]
        public string Points { get; set; }

        public bool ExamAllowed { get; set; }

        public int Counter { get; set; }

        [Display(Name = "Answer")]
        public string StudentExamAnswer { get; set; }

        [Display(Name = "Question")]
        public Question Question { get; set; }

        public StudentExamViewModel(int id, Student student, Exam exam, IEnumerable<StudentExamAnswer> studentExamAnswers, StudentExam studentExam)
        {
            Id = id;
            Student = student;
            Exam = exam;
            StudentExamAnswers = studentExamAnswers;
            if (studentExam.OnPoints != 0)
            {
                Points = studentExam.Points + " / " + studentExam.OnPoints;
            }
            if (DateTime.Now.Date == studentExam.Exam.Date && DateTime.Now.Hour >= studentExam.Exam.StartTime.Hours && DateTime.Now.Hour <= studentExam.Exam.EndTime.Hours && studentExam.OnPoints == 0)
            {
                ExamAllowed = true;
            } 
        }

        public StudentExamViewModel() { }

        public StudentExamViewModel(Student student, Exam exam)
        {
            Student = student;
            Exam = exam;
        }
    }
}
