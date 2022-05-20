using ComicBookStoreAPI.Domain.Models;
using Google.Apis.Auth;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth);
    }
}
