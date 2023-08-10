using JourneyApp.Infrastructure.Providers.Tokens;

using System.Net;

namespace JourneyApp.Journey.TokenManagement
{
    public class TokenManagerMiddleware : IMiddleware
    {

        private readonly ITokenProvider _tokenManager;

        public TokenManagerMiddleware(ITokenProvider tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenManager.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
