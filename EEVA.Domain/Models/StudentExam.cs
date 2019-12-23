﻿using System;
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
        public List<StudentExamAnswer> StudentExamAnswers { get; set; }

        public StudentExam(int id, Student student, Exam exam, List<StudentExamAnswer> studentExamAnswers)
        {
            Id = id;
            Student = student;
            Exam = exam;
            StudentExamAnswers = studentExamAnswers;
        }

        public StudentExam() { }
    }
}