﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class AnswerMultipleChoice : Answer
    {
        public string Answer { get; set; }
        public bool IsAnswerCorrect { get; set; }

        public AnswerMultipleChoice(string answer, bool isAnswerCorrect)
        {
            Answer = answer;
            IsAnswerCorrect = isAnswerCorrect;
        }
    }
}
