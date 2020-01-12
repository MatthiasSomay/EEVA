using EEVA.Domain.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public class StudentExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public Exam Exam { get; set; }

        public double Points { get; set; }

        public double OnPoints { get; set; }

        public IEnumerable<StudentExamAnswer> StudentExamAnswers { get; set; }

        public StudentExam(int id, Student student, Exam exam, IEnumerable<StudentExamAnswer> studentExamAnswers)
        {
            Id = id;
            Student = student;
            Exam = exam;
            StudentExamAnswers = studentExamAnswers;
        }

        public StudentExam() { }

        public StudentExam(Student student, Exam exam)
        {
            Student = student;
            Exam = exam;
        }

        public void CalculatePoints(EEVAContext context)
        {
            CalculateHelper ch = new CalculateHelper(context);
            ch.TotalPoints(this.Id);
            ch.CalculatePoints(this.Id);
        }
    }
}