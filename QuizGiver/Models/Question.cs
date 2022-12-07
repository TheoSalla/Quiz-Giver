﻿using System.Reflection.Metadata.Ecma335;

namespace QuizGiver.Models
{
    public class Question
    {
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public int Count { get; set; }
        public Question()
        {
            this.Category ??= "";
            this.Difficulty ??= "";
        }
    }
}
