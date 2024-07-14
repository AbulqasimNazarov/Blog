using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Models;
using BlogApp.Core.User.Models;

// using BlogApp.Core.User.Services.Base;
using BlogApp.Presentation.Verification.Base;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BlogApp.Presentation.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IDataProtector dataProtector;
        private readonly IValidator<RegistrationDto> userValidator;
        private readonly IEmailService emailService;

        public IdentityController(SignInManager<User> signInManager, UserManager<User> userManager, IValidator<RegistrationDto> userValidator, IDataProtectionProvider dataProtectionProvider, IEmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dataProtector = dataProtectionProvider.CreateProtector("identity");
            this.userValidator = userValidator;
            this.emailService = emailService;
        }

        [Route("/[controller]/[action]", Name = "LoginView")]
        public IActionResult Login(string? ReturnUrl)
        {
            var errorMessage = base.TempData["error"];
            ViewBag.ReturnUrl = ReturnUrl;

            if (errorMessage != null)
            {
                base.ModelState.AddModelError("All", errorMessage.ToString()!);
            }

            return base.View();
        }

        [Route("/[controller]/[action]", Name = "RegistrationView")]
        public IActionResult Registration()
        {
            if (TempData["error"] != null)
            {
                ModelState.AddModelError("All", "This Email already registered");
            }

            return base.View();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
        public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto)
        {
            try
            {
                var validationResult = userValidator.Validate(registrationDto);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View();
                }

                var tokenData = $"{registrationDto.Email}:{registrationDto.Name}";
                var token = dataProtector.Protect(tokenData);
                var confirmationLink = Url.Action("ConfirmEmail", "Identity", new { token }, Request.Scheme);
                var message = $"Please confirm your registration by clicking on the link: {HtmlEncoder.Default.Encode(confirmationLink)}";

                await emailService.SendEmailAsync(registrationDto.Email, "Confirm your email", message);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToRoute("RegistrationView");
            }

            return RedirectToRoute("ConfirmationView");
        }

        [HttpGet]
        [Route("/[controller]/[action]", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid token");
            }

            var tokenData = dataProtector.Unprotect(token);
            var dataParts = tokenData.Split(':');
            if (dataParts.Length != 2)
            {
                return BadRequest("Invalid token data");
            }

            var email = dataParts[0];
            var name = dataParts[1];

            var user = new User
            {
                Email = email,
                Name = name,
                UserName = name
            };

            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("\n", result.Errors.Select(error => error.Description)));
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.Name),
            new Claim(ClaimTypes.Role, "User")
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("ChooseTags", "Topic");
        }

        [HttpGet]
        [Route("[controller]/[action]", Name = "ConfirmationView")]
        public IActionResult Confirmation()
        {
            return View();
        }
    }

}