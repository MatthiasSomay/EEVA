using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public class StudentExam : Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Student Student { get; set; }
        public Exam Exam { get; set; }
        public List<StudentExamAnswer> StudentExamAnswers { get; set; }

        public StudentExam(Course course, DateTime date, TimeSpan start, TimeSpan end, List<Question> questions, List<StudentExam> studentExams, Student student, Exam exam, List<StudentExamAnswer> studentExamAnswers) : base(course, date, start, end, questions, studentExams)
        {
            this.Student = student;
            this.Exam = exam;
            this.StudentExamAnswers = studentExamAnswers;
        }
    }
}