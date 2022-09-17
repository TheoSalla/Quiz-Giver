using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    internal static class Decode
    {
        internal static Questions DecodeText(this Questions question)
        {
            for (int i = 0; i < question.Results.Count; i++)
            {
                question.Results[i].Question = System.Web.HttpUtility.HtmlDecode(question.Results[i].Question);
                question.Results[i].CorrectAnswer = System.Web.HttpUtility.HtmlDecode(question.Results[i].CorrectAnswer);
                for (int j = 0; j < question.Results[i].IncorrectAnswers.Count(); j++)
                {
                    question.Results[i].IncorrectAnswers[j] = System.Web.HttpUtility.HtmlDecode(question.Results[i].IncorrectAnswers[j]);
                }

            }
            return question;
        }

    }
}