﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class QuestionOpen : Question
    {
        public List<AnswerOpen> Answers { get; set; }
    }
}
