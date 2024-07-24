using Microsoft.AspNetCore.Mvc;
using QuizGiver.Models;
using QuizGiver.Repository;
using RestClientLib;
using Microsoft.Extensions.Logging;

namespace QuizGiver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IJsonToModel _questions;
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(IJsonToModel questions, IQuestionRepository questionRepository, IHttpClientFactory httpClientFactory, ILogger<QuestionController> logger)
        {
            Console.WriteLine("QuestionController constructor called");
            this._httpClientFactory = httpClientFactory;
            this._questions = questions;
            this._questionRepository = questionRepository;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetQuestion([FromQuery] Question q)
        {
            _logger.LogInformation("GetQuestion called with {Category} and {Difficulty}", q.Category, q.Difficulty);


            if (Enum.TryParse(q.Category, out Category category) && Enum.TryParse(q.Difficulty, out Difficulty difficulty))
            {
                _logger.LogInformation("Category and Difficulty parsed successfully");
                int count = (q.Count > 0) ? q.Count : 10;
                Questions listOfQuestions = await this._questions.GetQuestions(_httpClientFactory.CreateClient(), category, difficulty, count, HttpContext.Request.Cookies["session_token"]!);
                _logger.LogInformation("Response code: {ResponseCode}", listOfQuestions.ResponseCode);
                if (listOfQuestions.ResponseCode == 0)
                {
                    _logger.LogInformation("Getting questions from API");
                    return Ok
                    (listOfQuestions.Results);
                }
                else if
                (listOfQuestions.ResponseCode == 4)
                {
                    _logger.LogInformation("Session token expired or invalid quiz category");
                    _logger.LogInformation("Getting questions from Database");
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
