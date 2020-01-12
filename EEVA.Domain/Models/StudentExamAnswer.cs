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

        public StudentExam StudentExam { get; set; }

        public StudentExamAnswer()
        {

        }

        public StudentExamAnswer(int id, Question question, StudentExam studentExam)
        {
            Id = id;
            Question = question;
            StudentExam = studentExam;
        }

        public StudentExamAnswer(Question question, StudentExam studentExam)
        {
            Question = question;
            StudentExam = studentExam;
        }
    }
}