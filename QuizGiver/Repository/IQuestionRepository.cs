using QuizGiver.Models;

namespace QuizGiver.Repository
{
    public interface IQuestionRepository
    {
        Task<List<QuestionModel>> GetAllQuestionAsync();
    }
}
