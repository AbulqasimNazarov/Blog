using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Models;
using BlogApp.Core.User.Models;
using BlogApp.Core.UserTopic.Models;
using BlogApp.Infrastructure.Blog.Commands;
using BlogApp.Infrastructure.UserTopic.Queries;
using BlogApp.Presentation.Verification.Base;
using FluentValidation;
using MediatR;
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
        private readonly IValidator<LoginDto> userLoginValidator;
        private readonly IEmailService emailService;
        private readonly ISender sender;

        public IdentityController(ISender sender, IValidator<LoginDto> userLoginValidator, SignInManager<User> signInManager, UserManager<User> userManager, IValidator<RegistrationDto> userValidator, IDataProtectionProvider dataProtectionProvider, IEmailService emailService)
        {
            this.sender = sender;
            this.userLoginValidator = userLoginValidator;
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

        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "LoginEndpoint")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(loginDto.Email!);

                if (user == null)
                {
                    return BadRequest("Incorrect email or password!");
                }

                var validationResult = userLoginValidator.Validate(loginDto);
                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View();
                }

                var tokenData = $"{loginDto.Email}:{loginDto.Name}";
                var token = dataProtector.Protect(tokenData);
                var confirmationLink = Url.Action("ConfirmLogin", "Identity", new { token }, Request.Scheme);
                var message = $"Please confirm your login by clicking on the link: {HtmlEncoder.Default.Encode(confirmationLink!)}";

                await emailService.SendEmailAsync(loginDto.Email!, "Confirm your login", message);

                return RedirectToRoute("ConfirmationView");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/[controller]/[action]", Name = "ConfirmLogin")]
        public async Task<IActionResult> ConfirmLogin(string token)
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

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim("FullName", user.Name),
        new Claim(ClaimTypes.Role, "User")
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Получение выбранных топиков для пользователя
            var getAllTopicsByUserId = new GetAllTopicsByUserIdQuery
            {
                UserId = user.Id,
            };
            var userTopics = await this.sender.Send(getAllTopicsByUserId);
            var selectedTopicIds = userTopics.Select(ut => ut.Id).ToArray();

            return RedirectToAction("GetBlogsByTopic", "Blog", new { selectedTopicIds });
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
        public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto, [FromForm] int[] selectedTopicIds)
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
                var confirmationLink = Url.Action("ConfirmEmail", "Identity", new { token, selectedTopicIds }, Request.Scheme);
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
        public async Task<IActionResult> ConfirmEmail(string token, int[] selectedTopicIds)
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

            // Сохранение выбранных топиков для пользователя
            foreach (var topicId in selectedTopicIds)
            {
                var userTopic = new UserTopic
                {
                    TopicId = topicId,
                    UserId = user.Id
                };
                // Предположим, что у вас есть метод для добавления UserTopic в базу данных
                var create = new CreateCommand
                {
                    TopicId = topicId,
                    UserId = user.Id,
                };

                await this.sender.Send(create);


            }

            return RedirectToAction("ChooseTags", "Topic", new { selectedTopicIds });
        }

        [HttpGet]
        [Route("[controller]/[action]", Name = "ConfirmationView")]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
