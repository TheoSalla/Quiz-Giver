using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    public interface IJsonToModel
    {
        Task<Questions> GetQuestions(Category category, Difficulty difficulty, int amount, string sessionToken);
    }
}