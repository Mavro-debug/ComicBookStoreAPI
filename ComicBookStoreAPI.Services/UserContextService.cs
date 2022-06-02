using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ComicBookStoreAPI.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
        public string? GetUserId => User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}
