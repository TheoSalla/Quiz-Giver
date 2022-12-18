using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizGiver.Data;

namespace QuizGiver
{
    public class QuestionInfo
    {

        [Key]
        public Guid QuestionId { get; set; }
        [JsonPropertyName("category")]
        public string? Category { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("difficulty")]
        public string? Difficulty { get; set; }
        [JsonPropertyName("question")]
        public string? Question { get; set; }
        [JsonPropertyName("correct_answer")]
        public string? CorrectAnswer { get; set; }
        public List<IncorrectAnswer> IncorrectAnswers { get; set; }

    }
}
