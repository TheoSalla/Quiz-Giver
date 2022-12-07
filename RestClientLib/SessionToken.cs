namespace RestClientLib
{
    public static class SessionToken
    {
        public static string GenerateSessionToken()
        {
            HttpClient client = new();
            const string url = "https://opentdb.com/api_token.php?command=request";
            client.BaseAddress = new Uri(url);
            var response = client.GetStringAsync(client.BaseAddress).Result;

            return response.DeserializeAnonymousType(new { token = "" })!.token;
        }
        public static async Task<string> GenerateSessionTokenAsync(HttpClient client)
        {
            const string url = "https://opentdb.com/api_token.php?command=request";
            client.BaseAddress = new Uri(url);
            var response = await client.GetStringAsync(client.BaseAddress);
            return response.DeserializeAnonymousType(new { token = "" })!.token;
        }
    }
}
