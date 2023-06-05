namespace SuperHeroApi.Middleware
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
