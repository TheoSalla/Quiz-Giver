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
            const string c = "Entertainment: Books";
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
