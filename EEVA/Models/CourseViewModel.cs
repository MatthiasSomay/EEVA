﻿using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class CourseViewModel
    {
        public string Subtitle
        {
            get { return $"Details van {CourseName} - {CourseYear}"; }
        }

        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseYear { get; set; }
        public List<Question> Questions { get; set; }
        public List<Exam> Exams { get; set; }
    }
}
