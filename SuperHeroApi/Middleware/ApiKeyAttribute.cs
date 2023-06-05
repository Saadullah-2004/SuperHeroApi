using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.Middleware;

namespace SuperHeroApi.ApiKeyAttributes
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }

}
