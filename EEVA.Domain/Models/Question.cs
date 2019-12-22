﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public abstract class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string QuestionPhrase { get; set; }

        public Question()
        {

        }

        public Question(string question)
        {
            this.QuestionPhrase = question;
        }
    }
}