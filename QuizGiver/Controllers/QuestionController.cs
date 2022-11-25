using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Extensions;
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
        private string? category;
  


        public QuestionController(IJsonToModel questions, Token token, IQuestionRepository questionRepository)
        {
            this._questions = questions;
            this._token = token;
            this._questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion([FromQuery] Question q)
        {
            Response.Headers.SetCookie = $"category={q.Category}";
            Console.WriteLine($"First category {this.category}");
            if (finish)
            {
                return RedirectToAction(actionName: "GetQuestionFromDbBasedOnCategory");
            }
            
            if (Enum.TryParse(q.Category, out Category category) && Enum.TryParse(q.Difficulty, out Difficulty difficulty ))
            {
                int count = q.Count ?? 10;
                Questions listOfQuestions = await this._questions.GetQuestions(category, difficulty, count, _token.SessionToken);
                if (listOfQuestions.ResponseCode == 0)
                {
                    this.category = q.Category;
                    Console.WriteLine($"ENUM: {this.category}");
                    return Ok(listOfQuestions.Results);
                }
                else if (listOfQuestions.ResponseCode == 4)
                {
                    
                    finish = true;
                    Console.WriteLine("No more question");
                    return RedirectToAction(actionName: "GetQuestionFromDbBasedOnCategory");
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
        
        [HttpGet]
        [Route("db/category")]
        public async Task<IActionResult> GetQuestionFromDbBasedOnCategory()
        {
            
            // string c = GetValueFromCookie() ?? "music";
            string c = "computer";
            var questions = await _questionRepository.GetQuestionBasedOnCategory(c);
            return Ok(questions);

        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionModel q)
        {
            await _questionRepository.AddQuestion(q);
            return Ok();
        }

        private string? GetValueFromCookie()
        {
            var cookie = Request.Headers.Cookie;
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            for (int i = 0; i < cookie.ToString().Split("; ").Length; i++)
            {

                var s = cookie.ToString().Split("; ")[i].Split("=");
                string first = s[0];
                string last = s[1];
                cookies.Add(first, last);
            }
            if(cookies.TryGetValue("category", out string? value))
            {
                Console.WriteLine(value);
                return value;
            }

            return null;
        }
    }
}