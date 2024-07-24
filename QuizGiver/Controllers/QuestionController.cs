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
            int c;
            var isValidCategory = int.TryParse(q.Category, out c);
            if (!isValidCategory)
            {
                return BadRequest(new { Error = "Category must be a number" });
            }

            if (!Enum.IsDefined(typeof(Category), c))
            {
                var validCategories = new Dictionary<int, string>
                {
                    { 10, "book" },
                    { 11, "movie" },
                    { 12, "music" },
                    { 15, "videoGame" },
                    { 18, "computer" },
                    { 23, "history" },
                    { 32, "cartoon" }
                };

                var errorResponse = new
                {
                    Message = "Invalid category",
                    ValidCategories = validCategories
                };

                return BadRequest(errorResponse);
            }

            if (Enum.TryParse(q.Category, out Category category) && Enum.TryParse(q.Difficulty, out Difficulty difficulty))
            {
                int count = (q.Count > 0) ? q.Count : 10;
                Questions listOfQuestions = await this._questions.GetQuestions(_httpClientFactory.CreateClient(), category, difficulty, count, HttpContext.Request.Cookies["session_token"]!);
                if (listOfQuestions.ResponseCode == 0)
                {
                    return Ok
                    (listOfQuestions.Results);
                }
                else if
                (listOfQuestions.ResponseCode == 4)
                {
                    Console.WriteLine("Token has returned all possible questions for the specified query. Getting questions from the database");
                    return RedirectToAction(actionName: "GetQuestionFromDbBasedOnCategory");
                }
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
