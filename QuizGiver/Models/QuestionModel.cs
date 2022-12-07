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
            this.Question ??= "";
            this.CorrectAnswer ??= "";
            this.IncorrectAnswer ??= "";
            this.Difficulty ??= "";
            this.Category ??= "";
        }
    }
}
