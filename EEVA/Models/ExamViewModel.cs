﻿using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class ExamViewModel
    {
        //TODO annotation doesn't work editable
        [ReadOnly(true)]
        [Display(Name = "Id")]
        public int Id { get; private set; }

        [Display(Name = "Course")]
        public Course Course { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Display(Name = "Teacher")]
        public Teacher Teacher { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherFullName { get; set; }

        [Display(Name = "Exam Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Exam Questions")]
        public IEnumerable<Question> ExamQuestions { get; set; }

        [Display(Name = "Student Exams")]
        public IEnumerable<StudentExam> StudentExams { get; set; }

        //Used for Course dropdown in view
        public IEnumerable<Course> Courses { get; set; }
        public int CourseId { get; set; }

        //Used for Teacher dropdown in view
        public IEnumerable<Teacher> Teachers { get; set; }
        public int TeacherId { get; set; }

        public ExamViewModel(int id, Course course, Teacher teacher, DateTime date, TimeSpan startTime, TimeSpan endTime, IEnumerable<Question> examQuestions, IEnumerable<StudentExam> studentExams, IEnumerable<Course> courses, IEnumerable<Teacher> teachers)
        {
            Id = id;

            Course = course;
            if(Course != null)
            {
                CourseName = course.CourseName;
                CourseId = course.Id;
            }

            Teacher = teacher;
            if(teacher != null)
            {
                TeacherFullName = teacher.FullName;
                TeacherId = teacher.Id;
            }

            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            ExamQuestions = examQuestions;
            StudentExams = studentExams;
            Courses = courses;
            Teachers = teachers;
        }

        public ExamViewModel(IEnumerable<Course> courses, IEnumerable<Teacher> teachers)
        {
            Courses = courses;
            Teachers = teachers;
        }

        public ExamViewModel()
        {
           
        }
    }
}
