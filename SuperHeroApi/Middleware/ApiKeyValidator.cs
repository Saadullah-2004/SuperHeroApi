namespace SuperHeroApi.Middleware
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool IsValid(string apiKey)
        {
            string expectedApiKey = _configuration["x-api-key"];

            return apiKey == expectedApiKey;
        }
    }
}
