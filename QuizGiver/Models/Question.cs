using System.Text.Json.Serialization;

namespace QuizGiver.Models
{
    public class Question
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }


        [JsonPropertyName("difficulty")]
        public string? Difficulty { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        public Question()
        {
            this.Category ??= "";
            this.Difficulty ??= "";
        }
    }
}
