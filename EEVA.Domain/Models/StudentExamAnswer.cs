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

        public StudentExamAnswer()
        {

        }

        public StudentExamAnswer(int id, StudentExam studentExam)
        {
            Id = id;
            StudentExam = studentExam;
        }

        public StudentExamAnswer(StudentExam studentExam)
        {
            StudentExam = studentExam;
        }

        public virtual int CalculatePoints()
        {
            throw new System.NotImplementedException();
        }
    }
}