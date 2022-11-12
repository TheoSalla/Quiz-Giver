using Microsoft.EntityFrameworkCore;
using QuizGiver.Models;

namespace QuizGiver.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizContext _context;

        public QuestionRepository(QuizContext context)
        {
            _context = context;
        }
        public async Task<List<QuestionModel>> GetAllQuestionAsync()
        {
            var records = await _context.Questions.Select(x => new QuestionModel()
            {
                Id = x.Id,
                Question= x.Question,
                Category= x.Category,
                CorrectAnswer= x.CorrectAnswer,
                Difficulty= x.Difficulty,
                IncorrectAnswer= x.IncorrectAnswer,

            }).ToListAsync();

            return records;
        }
    }
}
