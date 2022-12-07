using QuizGiver.Models;

namespace QuizGiver.Repository
{
    public interface IQuestionRepository
    {
        Task<List<QuestionModel>> GetAllQuestionAsync();
        Task<QuestionModel> AddQuestion(QuestionModel question);
        Task<List<QuestionModel>> GetQuestionBasedOnCategory(string category);
    }
}
