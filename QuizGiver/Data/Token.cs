using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizGiver
{
    public class Token
    {
        public string SessionToken;
        public Token()
        {
            var token = RestClientLib.SessionToken.GenerateSessionToken();
            Console.WriteLine($"Generating token: {token}");
            this.SessionToken = token;
        }
    }
}