using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly Token _token;
        private readonly IQuestionRepository _questionRepository;
        private bool finish;

        public QuestionController(IJsonToModel questions, Token token, IQuestionRepository questionRepository)
        {
            this._questions = questions;
            this._token = token;
            this._questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion([FromQuery] Question q)
        {
            if(finish)
            {
                return RedirectToAction(actionName: "GetQuestionFromDb");
            }
            
            if (Enum.TryParse(q.Category, out Category category) && Enum.TryParse(q.Difficulty, out Difficulty difficulty ))
            {
                int count = q.Count ?? 10;
                Questions listOfQuestions = await this._questions.GetQuestions(category, difficulty, count, _token.SessionToken);
                if (listOfQuestions.ResponseCode == 0)
                {
                    return Ok(listOfQuestions);
                }
                else if (listOfQuestions.ResponseCode == 4)
                {
                    finish = true;
                    Console.WriteLine("No more question");
                    return RedirectToAction(actionName: "GetQuestionFromDb");
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("db")]
        public async Task<IActionResult> GetQuestionFromDb()
        {
            var questions = await _questionRepository.GetAllQuestionAsync();
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