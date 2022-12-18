using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizGiver.Data
{
    public class IncorrectAnswer
    {
        [Key]
        public Guid IncorrectAnswerId { get; set; } 
        public string Answer { get; set; }
        public Guid QuestionId { get; set; }
    }
}