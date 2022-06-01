using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace ComicBookStoreAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;
        public AccountService(IConfiguration aConfiguration)
        {
            _config = aConfiguration;
        }

        public string GenerateUserRegistrationEmialBody(string name, string confirmationLink)
        {
            return  $"Welcome {name}<br>" +
                    $"You were successfully registered!!!<br>" +
                    $"Please, confirm your email using the link:" +
                    $"{confirmationLink}" +
                    $"Best regards,";
        }

        public string GenerateUserRegistrationEmialBody(string name, string confirmationLink, string temporaryPassword)
        {
            return $"Welcome {name}<br>" +
                    $"You were successfully registered!!!<br>" +
                    $"Your temporary password: {temporaryPassword}" +
                    $"It is recommended that you change your password after logging." +
                    $"Please, confirm your email using the link:" +
                    $"{confirmationLink}" +
                    $"Best regards,";
        }

        public string GenerateTemporaryPassword(string name, string lastName)
        {
            StringBuilder passwordBuilder = new StringBuilder();

            var namePart = name.Trim();

            passwordBuilder.Append(char.ToUpper(namePart[0]));
            passwordBuilder.Append(namePart.Substring(1, 2));
            passwordBuilder.Append("!");

            var lastNamePart = lastName.Trim();
            passwordBuilder.Append(char.ToUpper(lastNamePart[0]));
            passwordBuilder.Append(lastNamePart.Substring(1, 2));

            Random rnd = new Random();

            passwordBuilder.Append(rnd.Next(10, 99));

            return passwordBuilder.ToString();
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
