using System.Security.Claims;


namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IUserContextService
    {
        string? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}
