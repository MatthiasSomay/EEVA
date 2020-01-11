using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Question Question { get; set; }

        public StudentExamAnswer()
        {

        }

        public StudentExamAnswer(int id, Question question)
        {
            Id = id;
            Question = question;
        }
    }
}