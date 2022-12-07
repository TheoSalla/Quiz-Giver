using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizGiver
{
    public class QuestionInfo
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }

        public QuestionInfo()
        {
            this.Question ??= "";
            this.CorrectAnswer ??= "";
            this.IncorrectAnswer ??= "";
            this.Difficulty ??= "";
            this.Category ??= "";
        }
    }
}