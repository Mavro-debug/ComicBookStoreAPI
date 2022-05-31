using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComicBookStoreAPI.Controllers
{
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailService emailService,
            IAccountService accountService, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            _logger.LogInformation("Register action invoked");

            var findEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (findEmail != null)
            {
                return Unauthorized("email taken");
            }

            var newUser = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };

            var resoult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (resoult.Succeeded)
            {
                var roleResoult = await _userManager.AddToRoleAsync(newUser, "Client");

                if (!roleResoult.Succeeded)
                {
                    throw new AccountException("Unable to assign Client role to a new user");
                }


                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                var confirmationLink = Url.Action("confirmEmail", "account",
                    new { userId = newUser.Id, token = token }, Request.Scheme);


                _logger.LogInformation($"User with Id = {newUser.Id} was successfully registered");

                await _emailService.SendEmailAsync(new MailRequest(newUser.Email, $"Witaj {newUser.UserName}", $"Twoja rejestracja przebiegła pomyślnie!!! <br> Potwierdz email linkiem: <br> {confirmationLink}"));

                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            if (userId == null || token == null)
            {
                throw new Exception(); //change to custom exception
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException($"User with Id:{userId} was not found");
            }

            var resoult = await _userManager.ConfirmEmailAsync(user, token);

            if (!resoult.Succeeded)
            {
                throw new Exception($"Unable ro confirm User's with Id:{userId} email");
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var resoult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (resoult.Succeeded)
            {
                _logger.LogInformation($"User with Id = {user.Id} was successfully signed in");

                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("externalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            _logger.LogInformation("External authentication process involved");

            var payload = await _accountService.VerifyGoogleToken(externalAuth);
            if (payload == null)
                throw new InvalidExternalAuthenticationException("Google token could not be verifyed");

            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Email = payload.Email,
                        UserName = payload.GivenName
                    };
                    var resoult = await _userManager.CreateAsync(user);

                    await _userManager.AddLoginAsync(user, info);

                    if (resoult.Succeeded)
                    {
                        _logger.LogInformation($"User with Id = {user.Id} was successfully created");

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        await _userManager.ConfirmEmailAsync(user, token);

                    }
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
            {
                throw new InvalidExternalAuthenticationException("User was not able to be created");
            }

            await _signInManager.SignInAsync(user, true);

            _logger.LogInformation($"User with Id = {user.Id} was successfully signed in");

            return Ok();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var user = await _userManager.GetUserAsync(User);

            await _signInManager.SignOutAsync();

            _logger.LogInformation($"User with Id = {userId} was successfully signed out");

            return Ok();

        }
    }
}
