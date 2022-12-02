using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class Questions
    {
        public int ResponseCode { get; set; }

        public List<Results> Results { get; set; }

        public Questions()
        {
            if(Results == null)
            {
                Results = new();
            }   
        }
    }

    public class Results
    {
        public string Category { get; set; }

        public string Type { get; set; }

        public string Difficulty { get; set; }

        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> IncorrectAnswers { get; set; }

        public Results()
        {
            if(Category == null)
            {
                Category = "";
            }
            if(Type == null)
            {
                Type = "";
            }
            if(Difficulty == null)
            {
                Difficulty = "";
            }
            if(CorrectAnswer == null)
            {
                CorrectAnswer = "";
            }
            if(Question == null)
            {
                Question = "";
            }
            if(IncorrectAnswers == null)
            {
                IncorrectAnswers = new();
            }
       
        }


    }
}