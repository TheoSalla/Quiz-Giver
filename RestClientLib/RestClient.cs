using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    internal class RestClient
    {
        HttpClient client = new();

        internal Stream GetData(Category category = 0, Difficulty difficulty = 0, int amount = 10, string sessionToken = "")
        {
            System.Console.WriteLine("Category from lib: " + category);
            var route = $"?amount={amount}&category={(int)category}&difficulty={difficulty}&token={sessionToken}";
            client.BaseAddress = new Uri($"https://opentdb.com/api.php{route}");
            var response = client.GetStreamAsync(client.BaseAddress).Result;
            return response;
        }
        internal Stream GetData2()
        {
            Console.WriteLine("Category from lib GetData2: " + _settings.category);
            client.BaseAddress = new Uri($"https://opentdb.com/api.php{_route}");
            var response = client.GetStreamAsync(client.BaseAddress).Result;
            return response;
        }

        private Settings _settings;
        private string categoryParam = "&category=";
        private string difficultyParam = "&difficulty=";
        private string url = "https://opentdb.com/api.php?";
        private string _route = "";


        public RestClient(Settings settings)
        {

            if (settings.difficulty == Difficulty.everyDifficulty)
            {
                difficultyParam = $"&difficulty=0";
            }
            else
            {
                difficultyParam = $"&difficulty={settings.difficulty}";
            }
            _route = $"?amount={settings.amount}&category={(int)settings.category}{difficultyParam}&token={settings.sessionToken}";

            _settings = settings;
        }

    }
}