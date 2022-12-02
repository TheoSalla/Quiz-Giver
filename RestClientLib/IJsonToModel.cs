using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    public interface IJsonToModel
    {
        // Task<Questions> GetQuestioSystem.Net.Http.IHttpClientFactory _client, ns(Category category, Difficulty difficulty, int amount, string sessionToken);
        // Task<Questions> GetQuestions(HttpClient httpClient, Category category, Difficulty difficulty, int amount, string sessionToken);
        Task<Questions> GetQuestions(HttpClient client, Category category, Difficulty difficulty, int count, string sessionToken);
    }
}