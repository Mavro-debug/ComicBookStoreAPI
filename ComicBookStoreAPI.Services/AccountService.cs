using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace ComicBookStoreAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;
        public AccountService(IConfiguration aConfiguration)
        {
            _config = aConfiguration;
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _config.GetSection("ExternalAuthentication").GetSection("Google").GetSection("ClientId").Value }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }
    }
}
