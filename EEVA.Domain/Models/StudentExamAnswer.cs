using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EEVA.Domain.Models
{
    public class StudentExamAnswer : ICalculatePoints
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public StudentExam StudentExam { get; set; }

        public Question Question { get; set; }

        public StudentExamAnswer()
        {

        }

        public StudentExamAnswer(int id, StudentExam studentExam)
        {
            Id = id;
            StudentExam = studentExam;
        }

        public StudentExamAnswer(Question question, StudentExam studentExam)
        {
            StudentExam = studentExam;
            Question = question;
        }

        public virtual double CalculatePoints()
        {
            throw new System.NotImplementedException();
        }
    }
}