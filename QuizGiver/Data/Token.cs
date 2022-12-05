namespace QuizGiver
{
    public class Token
    {
        public string SessionToken;
        private readonly HttpClient _client;
        public Token(HttpClient client)
        {
            this._client = client;
            if(SessionToken == null)
            {
              SessionToken = "";
            }
        }
        public async Task GenerateTokenAsync()
        {
            SessionToken = await RestClientLib.SessionToken.GenerateSessionTokenAsync(_client);
        } 
    }
}