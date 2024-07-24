using Microsoft.AspNetCore.Mvc;
using QuizGiver.Models;
using QuizGiver.Repository;
using RestClientLib;

namespace QuizGiver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IJsonToModel _questions;
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public QuestionController(IJsonToModel questions, IQuestionRepository questionRepository, IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            this._questions = questions;
            this._questionRepository = questionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetQuestion([FromQuery] Question q)
        {
            Category categoryEnum;

            if (Enum.TryParse(q.Category, out Category cat))
            {
                categoryEnum = (Category)Enum.Parse(typeof(Category), cat.ToString());
            }
            else if (string.IsNullOrEmpty(q.Category))
            {
                Console.WriteLine("Category not provided. Generating random category");
                Array values = Enum.GetValues(typeof(Category));
                Random rnd = new();
                Category randomCategory = (Category)values.GetValue(rnd.Next(values.Length));
                categoryEnum = randomCategory;
            }
            else
            {
                Console.WriteLine("Invalid category provided");
                var errorResponse = new
                {
                    Message = "Invalid category",
                    ValidCategories = $"{Category.book}, {Category.movie}, {Category.music}, {Category.videoGame}, {Category.computer}, {Category.history}, {Category.cartoon}"
                };

                return BadRequest(errorResponse);
            }

            int count = (q.Count > 0) ? q.Count : 10;
            Difficulty difficulty;
            if (!Enum.TryParse(q.Difficulty, out difficulty))
            {
                difficulty = Difficulty.everyDifficulty;
            }

            try
            {
                Questions listOfQuestions = await this._questions.GetQuestions(_httpClientFactory.CreateClient(), categoryEnum, difficulty, count, HttpContext.Request.Cookies["session_token"]!);
                if (listOfQuestions.ResponseCode == 0)
                {
                    return Ok(listOfQuestions.Results);
                }
                else if
                (listOfQuestions.ResponseCode == 4)
                {
                    Console.WriteLine("Token has returned all possible questions for the specified query. Getting questions from the database");
                    return RedirectToAction(actionName: "GetQuestionFromDbBasedOnCategory");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }
        [HttpGet]
        [Route("db")]
        public async Task<IActionResult> GetQuestionFromDb()
        {
            var questions = await
             _questionRepository.GetAllQuestionAsync();
            return Ok(questions);
        }
        [HttpGet]
        [Route("db/category")]
        public async Task<IActionResult> GetQuestionFromDbBasedOnCategory()
        {
            // string c = GetValueFromCookie() ?? "music";
            const string c = "computer";
            var questions = await _questionRepository.GetQuestionBasedOnCategory(c);
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionModel q)
        {
            await _questionRepository.AddQuestion(q);
            return Ok();
        }
    }
}
