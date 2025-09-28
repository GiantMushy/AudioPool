
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AudioPool.WebApi.Attributes
{
    public class ApiTokenAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private const string API_TOKEN_HEADER = "api-token";
        private const string VALID_API_TOKEN = "AudioPoolSecretToken2025"; 
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the api-token header exists
            if (!context.HttpContext.Request.Headers.TryGetValue(API_TOKEN_HEADER, out var tokenValues))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "API token is required" });
                return;
            }

            var token = tokenValues.FirstOrDefault();

            // Check if token is null or empty
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "API token cannot be empty" });
                return;
            }

            // Validate the token
            if (token != VALID_API_TOKEN)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Invalid API token" });
                return;
            }

            // Token is valid, allow the request to proceed
        }
    }
}

/* this is rdundent code but usefull for next assignemnt. I overshot the requierments
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AudioPool.WebApi.Attributes
{
    public class ApiTokenAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private const string AUTH_COOKIE = "AudioPool-Auth";
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the authentication cookie exists
            if (!context.HttpContext.Request.Cookies.TryGetValue(AUTH_COOKIE, out var authValue))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Authentication required. Please log in." });
                return;
            }

            // Check if cookie value is null or empty
            if (string.IsNullOrEmpty(authValue))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Invalid authentication cookie." });
                return;
            }

            // Validate the authentication token (in a real app, this would decrypt/verify a JWT or session token)
            if (!IsValidAuthToken(authValue))
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Authentication expired or invalid. Please log in again." });
                return;
            }

            // Authentication successful, allow the request to proceed
        }

        private bool IsValidAuthToken(string authToken)
        {
            // Simple validation - in a real app, you'd verify JWT or check session store
            // For this assignment, we'll use a simple format: "user:{email}:timestamp"
            try
            {
                var parts = authToken.Split(':');
                if (parts.Length != 3 || parts[0] != "user")
                    return false;

                // Check if token is not older than 24 hours
                if (long.TryParse(parts[2], out var timestamp))
                {
                    var tokenTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                    return DateTimeOffset.UtcNow.Subtract(tokenTime).TotalHours < 24;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
*/
