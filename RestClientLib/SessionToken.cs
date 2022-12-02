using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class SessionToken
    {

        public static string GenerateSessionToken()
        {
            HttpClient client = new();
            string url = "https://opentdb.com/api_token.php?command=request";
            client.BaseAddress = new Uri(url);
            var response = client.GetStringAsync(client.BaseAddress).Result;

            return JsonSerializerExtensions.DeserializeAnonymousType(response, new { token = "" })!.token;

        }
        public static string GenerateSessionToken(HttpClient client)
        {
            string url = "https://opentdb.com/api_token.php?command=request";
            client.BaseAddress = new Uri(url);
            var response = client.GetStringAsync(client.BaseAddress).Result;


            return JsonSerializerExtensions.DeserializeAnonymousType(response, new { token = "" })!.token;

        }


    }
}