﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EEVA.Domain.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Course Course { get; set; }

        [Required]
        public Teacher Teacher { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [Required]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        [Required] 
        public TimeSpan EndTime { get; set; }

        public List<Question> ExamQuestions { get; set; }
        public List<StudentExam> StudentExams { get; set; }

        public Exam()
        {
        }

        public Exam(int id, Course course, Teacher teacher, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            Id = id;
            Course = course;
            Teacher = teacher;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Exam(int id, Course course, Teacher teacher, DateTime date, TimeSpan startTime, TimeSpan endTime, List<Question> examQuestions, List<StudentExam> studentExams)
        {
            Id = id;
            Course = course;
            Teacher = teacher;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            ExamQuestions = examQuestions;
            StudentExams = studentExams;
        }
    }
}
