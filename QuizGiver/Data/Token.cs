namespace QuizGiver
{
    public class Token
    {
        public string SessionToken;
        private readonly IHttpClientFactory _httpClientFactory;

        public Token(IHttpClientFactory httpClientFactory)
        {
            SessionToken ??= "";

            this._httpClientFactory = httpClientFactory;
        }
        public async Task GenerateTokenAsync() => SessionToken = await RestClientLib.SessionToken.GenerateSessionTokenAsync(_httpClientFactory.CreateClient());
    }
}
