using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    internal class RestClient
    {
        readonly HttpClient client = new();

        internal Stream GetData(Category category = 0, Difficulty difficulty = 0, int amount = 10, string sessionToken = "")
        {
            var route = $"?amount={amount}&category={(int)category}&difficulty={difficulty}&token={sessionToken}";
            client.BaseAddress = new Uri($"https://opentdb.com/api.php{route}");
            var response = client.GetStreamAsync(client.BaseAddress).Result;
            return response;
        }
        internal Stream GetData2()
        {
            client.BaseAddress = new Uri($"https://opentdb.com/api.php{_route}");
            var response = client.GetStreamAsync(client.BaseAddress).Result;
            return response;
        }
        internal async Task<Stream> GetRequestToQuizApi(HttpClient client)
        {
            client.BaseAddress = new Uri($"https://opentdb.com/api.php{_route}");
            var response = await client.GetStreamAsync(client.BaseAddress);
            return response;
        }

        // private string categoryParam = "&category=";
        private readonly string difficultyParam = "&difficulty=";
        // private string url = "https://opentdb.com/api.php?";
        private readonly string _route = "";

        public RestClient(Settings settings)
        {
            if (settings.Difficulty == Difficulty.everyDifficulty)
            {
                difficultyParam = "&difficulty=0";
            }
            else
            {
                difficultyParam = $"&difficulty={settings.Difficulty}";
            }
            _route = $"?amount={settings.Amount}&category={(int)settings.Category}{difficultyParam}&token={settings.SessionToken}";
        }
    }
}
