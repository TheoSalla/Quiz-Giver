using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizGiver
{
    public class Token
    {
        public string SessionToken;
        private readonly HttpClient _client;
        public Token(HttpClient client)
        {
            this._client = client;
            var token = RestClientLib.SessionToken.GenerateSessionToken(_client);
            Console.WriteLine($"Generating token: {token}");
            this.SessionToken = token;
            Hello();
        }

        private void Hello()
        {
            Console.WriteLine("Hello from token class!");
        }
        
    }
}