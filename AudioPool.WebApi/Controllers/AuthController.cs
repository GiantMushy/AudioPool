/*using Microsoft.AspNetCore.Mvc;

namespace AudioPool.WebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private const string AUTH_COOKIE = "AudioPool-Auth";
        
        private readonly Dictionary<string, string> _validUsers = new()
        {
            { "odinns24@ru.is", "password123" },
            { "admin@audiopool.com", "admin123" },
            { "test@audiopool.com", "test123" }
        };

        /// <summary>
        /// Login with username and password
        /// POST /auth/login
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Email and password are required" });
            }

            // Validate credentials
            if (!_validUsers.TryGetValue(request.Email.ToLower(), out var validPassword) || 
                validPassword != request.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Create authentication token (simple format for this assignment)
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var authToken = $"user:{request.Email}:{timestamp}";

            // Set the authentication cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Prevent JavaScript access for security
                Secure = false, // Set to true in production with HTTPS
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(24), // Cookie expires in 24 hours
                Path = "/"
            };

            Response.Cookies.Append(AUTH_COOKIE, authToken, cookieOptions);

            return Ok(new 
            { 
                message = "Login successful", 
                user = request.Email,
                expiresAt = DateTime.UtcNow.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss UTC")
            });
        }

        /// <summary>
        /// Logout - removes the authentication cookie
        /// POST /auth/logout
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(AUTH_COOKIE);
            return Ok(new { message = "Logged out successfully" });
        }

        /// <summary>
        /// Check authentication status
        /// GET /auth/status
        /// </summary>
        [HttpGet("status")]
        public IActionResult GetAuthStatus()
        {
            if (Request.Cookies.TryGetValue(AUTH_COOKIE, out var authValue) && 
                IsValidAuthToken(authValue))
            {
                var userEmail = ExtractEmailFromToken(authValue);
                return Ok(new 
                { 
                    authenticated = true, 
                    user = userEmail,
                    message = "User is authenticated" 
                });
            }

            return Ok(new { authenticated = false, message = "User is not authenticated" });
        }

        /// <summary>
        /// Get list of valid test users (for development/testing only)
        /// GET /auth/test-users
        /// </summary>
        [HttpGet("test-users")]
        public IActionResult GetTestUsers()
        {
            var users = _validUsers.Keys.Select(email => new { email, password = _validUsers[email] });
            return Ok(new 
            { 
                message = "Test users for development",
                users = users,
                note = "In production, this endpoint should be removed"
            });
        }

        private bool IsValidAuthToken(string authToken)
        {
            try
            {
                var parts = authToken.Split(':');
                if (parts.Length != 3 || parts[0] != "user")
                    return false;

                // Check if user still exists in our valid users
                if (!_validUsers.ContainsKey(parts[1].ToLower()))
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

        private string ExtractEmailFromToken(string authToken)
        {
            try
            {
                var parts = authToken.Split(':');
                return parts.Length >= 2 ? parts[1] : "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}*/