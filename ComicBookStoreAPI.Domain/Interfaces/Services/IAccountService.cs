using ComicBookStoreAPI.Domain.Models;
using Google.Apis.Auth;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth);
        string GenerateTemporaryPassword(string name, string lastName);
        string GenerateUserRegistrationEmialBody(string name, string confirmationLink);
        string GenerateUserRegistrationEmialBody(string name, string confirmationLink, string temporaryPassword);
    }
}
