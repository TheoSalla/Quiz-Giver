using Microsoft.EntityFrameworkCore;
using QuizGiver.Models;

namespace QuizGiver.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizContext _context;
        public QuestionRepository(QuizContext context) => _context = context;
        public async Task<List<QuestionModel>> GetAllQuestionAsync()
        {
            List<QuestionModel> records = await _context.Questions.Select(x => new QuestionModel()
            {
                Id = x.Id,
                Question = x.Question,
                Category = x.Category,
                CorrectAnswer = x.CorrectAnswer,
                Difficulty = x.Difficulty,
                IncorrectAnswer = x.IncorrectAnswer,

            }).ToListAsync();
            return records;
        }
        public async Task<List<QuestionModel>> GetQuestionBasedOnCategory(string category)
        {
            var records = await _context.Questions.Select(x => new QuestionModel()
            {
                Id = x.Id,
                Question = x.Question,
                Category = x.Category,
                CorrectAnswer = x.CorrectAnswer,
                Difficulty = x.Difficulty,
                IncorrectAnswer = x.IncorrectAnswer,

            }).Where(y => y.Category == category).ToListAsync();
            return records;
        }
        public async Task<QuestionModel> AddQuestion(QuestionModel question)
        {
            QuestionInfo q = new()
            {
                Id = question.Id,
                Question = question.Question,
                Category = question.Category,
                CorrectAnswer = question.CorrectAnswer,
                Difficulty = question.Difficulty,
                IncorrectAnswer = question.IncorrectAnswer,
            };
            await _context.AddAsync(q);
            await _context.SaveChangesAsync();
            return question;
        }
    }
}