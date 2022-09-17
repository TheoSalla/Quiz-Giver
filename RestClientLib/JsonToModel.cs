using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class JsonToModel : IJsonToModel
    {
        public async Task<Questions> GetQuestions(Category category = 0, Difficulty difficulty = 0, int amount = 10, string sessionToken = "")
        {
            Settings settings = new();
            settings.amount = amount;
            settings.category = category;
            settings.difficulty = difficulty;
            settings.sessionToken = sessionToken;

            RestClient client = new(settings);
            
            var data = client.GetData2();

            JsonSerializerOptions serializerOptions = new()
            {
                PropertyNamingPolicy = new NamingPolicy()
            };
            Questions? questions = new();
            try
            {
                questions = JsonSerializer.DeserializeAsync<Questions>(data, serializerOptions).Result;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error message: " + ex.Message);
            }

            //Decode.DecodeJson(q1);
            questions.DecodeText();

            return questions;

        }
    }
}