using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace RestClientLib
{
    public class JsonToModel : IJsonToModel
    {
        public async Task<Questions> GetQuestions(HttpClient httpClient, Category category = 0, Difficulty difficulty = 0, int amount = 10, string sessionToken = "")
        {
            Settings settings = new();
            settings.amount = amount;
            settings.category = category;
            settings.difficulty = difficulty;
            settings.sessionToken = sessionToken;

            RestClient client = new(settings);
            
            var data = await client.GetRequestToQuizApi(httpClient);

            JsonSerializerOptions serializerOptions = new()
            {
                PropertyNamingPolicy = new NamingPolicy()
            };
            Questions? questions = new();
            try
            {
                questions = await JsonSerializer.DeserializeAsync<Questions>(data, serializerOptions);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error message: " + ex.Message);
            }
            questions!.DecodeText();

            return questions!;

        }
    }
}