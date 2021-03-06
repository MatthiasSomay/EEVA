﻿using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class QuestionMultipleChoiceViewModel : QuestionViewModel
    {
 
        [Display(Name = "Answers")]
        public IEnumerable<AnswerMultipleChoice> Answers { get; set; }

        public QuestionMultipleChoiceViewModel(int id, string questionPhrase, Course course, IEnumerable<AnswerMultipleChoice> answers, IEnumerable<Course> courses) : base(id, questionPhrase, course, courses)
        {
            Answers = answers;
        }
    }
}
