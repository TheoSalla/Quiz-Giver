namespace QuizGiver.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string IncorrectAnswer { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }
        public QuestionModel()
        {
            if (this.Question == null)
            {
                this.Question = "";
            }
            if (this.CorrectAnswer == null)
            {
                this.CorrectAnswer = "";
            }
            if (this.IncorrectAnswer == null)
            {
                this.IncorrectAnswer = "";
            }
            if (this.Difficulty == null)
            {
                this.Difficulty = "";
            }
            if (this.Category == null)
            {
                this.Category = "";
            }
        }
    }
}

