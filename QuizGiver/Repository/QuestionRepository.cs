using Microsoft.EntityFrameworkCore;
using QuizGiver.Models;

namespace QuizGiver.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionsDbContext _context;
        public QuestionRepository(QuestionsDbContext context) => _context = context;
        public async Task<List<QuestionModel>> GetAllQuestionAsync()
        {

            
            List<QuestionModel> records = await _context.Questions.Select(x => new QuestionModel()
            {
                Id = x.QuestionId,
                Question = x.Question,
                Category = x.Category,
                CorrectAnswer = x.CorrectAnswer,
                Difficulty = x.Difficulty,
                IncorrectAnswers = x.IncorrectAnswers
            }).ToListAsync();
            return records;
        }
        public async Task<List<QuestionModel>> GetQuestionBasedOnCategory(string category)
        {
            var records = await _context.Questions.Select(x => new QuestionModel()
            {
                Id = x.QuestionId,
                Question = x.Question,
                Category = x.Category,
                CorrectAnswer = x.CorrectAnswer,
                Difficulty = x.Difficulty,
                IncorrectAnswers = x.IncorrectAnswers
            }).Where(y => y.Category == category).ToListAsync();
            return records;
        }
        public async Task<QuestionModel> AddQuestion(QuestionModel question)
        {
            QuestionInfo q = new()
            {
                QuestionId = question.Id,
                Question = question.Question,
                Category = question.Category,
                CorrectAnswer = question.CorrectAnswer,
                Difficulty = question.Difficulty,
                IncorrectAnswers = question.IncorrectAnswers
            };
            await _context.AddAsync(q);
            await _context.SaveChangesAsync();
            return question;
        }
    }
}
