using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizGiver.Models;
using RestClientLib;

namespace QuizGiver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {

        private readonly IJsonToModel questions;
        private readonly Token token;

        public QuestionController(IJsonToModel questions, Token token)
        {
            this.questions = questions;
            this.token = token;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion([FromQuery] Question q)
        {       
            if(Enum.TryParse(q.Category, out Category category) && Enum.TryParse(q.Difficulty, out Difficulty difficulty ))
            {
                int count = q.Count ?? 10;
                RestClientLib.Questions listOfQuestions = new();
                listOfQuestions = await this.questions.GetQuestions(category, difficulty, count, token.SessionToken);
                if (listOfQuestions.ResponseCode == 0)
                {
                    return Ok(listOfQuestions);
                }
                else if (listOfQuestions.ResponseCode == 4)
                {
                    return Ok("No more questions");
                }
            }
            return BadRequest();
        }
    }
}