using System.Linq;
using System.Security.Claims;
using Application.SystemCourse.Contracts;
using Microsoft.AspNetCore.Http;

namespace Security.SystemCourse.TokenSecurity
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string ObtainUserSession()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}