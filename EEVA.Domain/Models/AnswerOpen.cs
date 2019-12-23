using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models
{
    public class AnswerOpen : Answer
    {
        public string Keyword { get; set; }

        public AnswerOpen(string keyword)
        {
            Keyword = keyword;
        }
    }
}