using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Models;
// using BlogApp.Core.User.Services.Base;
using BlogApp.Presentation.Verification.Base;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BlogApp.Presentation.Controllers
{
    public class IdentityController : Controller
    {
        // public IUserService userService;
        private readonly IDataProtector dataProtector;
        private readonly IValidator<RegistrationDto> userValidator;
        private readonly IEmailService emailService;

        public IdentityController(IValidator<RegistrationDto> userValidator, /*IUserService userService*/ IDataProtectionProvider dataProtectionProvider, IEmailService emailService)
        {
            // this.userService = userService;
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

        // [Route("api/[controller]/[action]", Name = "LoginEndPoint")]
        // [HttpPost]
        // public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        // {
        //     var foundUser = await userService.GetSignedUpUser(loginDto);
        //     if (foundUser == null || foundUser.Email != loginDto.Email)
        //     {
        //         base.TempData["error"] = "Incorrect login or password!";
        //         return base.RedirectToRoute("LoginView");
        //     }

        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, foundUser.Email),
        //         new Claim("FullName", foundUser.Name),
        //         new Claim(ClaimTypes.Role, "User")
        //     };

        //     var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        //     return base.RedirectToAction(controllerName: "Home", actionName: "Index");
        // }

        // [Route("/[controller]/[action]", Name = "RegistrationView")]
        // public IActionResult Registration()
        // {
        //     if (TempData["error"] != null)
        //     {
        //         ModelState.AddModelError("All", "This Email already registered");
        //     }

        //     return base.View();
        // }

        // [HttpPost]
        // [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
        // public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto)
        // {
        //     try
        //     {
        //         var validationResult = userValidator.Validate(registrationDto);
        //         if (!validationResult.IsValid)
        //         {
        //             foreach (var error in validationResult.Errors)
        //             {
        //                 ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //             }
        //             return View();
        //         }

        //         await this.userService.CreateAsync(registrationDto);

                
        //         var token = dataProtector.Protect(registrationDto.Email);
        //         var confirmationLink = Url.Action("ConfirmEmail", "Identity", new { token }, Request.Scheme);
        //         var message = $"Please confirm your registration by clicking on the link: {HtmlEncoder.Default.Encode(confirmationLink)}";

        //         await emailService.SendEmailAsync(registrationDto.Email, "Confirm your email", message);
        //     }
        //     catch (Exception ex)
        //     {
        //         TempData["error"] = ex.Message;
        //         return base.RedirectToRoute("RegistrationView");
        //     }

        //     return base.RedirectToRoute("ConfirmationView");
        // }

        // [HttpGet]
        // [Route("/[controller]/[action]", Name = "ConfirmEmail")]
        // public async Task<IActionResult> ConfirmEmail(string token)
        // {
        //     var email = dataProtector.Unprotect(token);
        //     var user = await userService.GetUserByEmailAsync(email);

        //     if (user == null)
        //     {
        //         return BadRequest("Invalid token");
        //     }

        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, user.Email),
        //         new Claim("FullName", user.Name),
        //         new Claim(ClaimTypes.Role, "User")
        //     };

        //     var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        //     return base.RedirectToAction(controllerName: "Topic", actionName: "ChooseTags");
        // }

        // [HttpGet]
        // [Route("/api/[controller]/[action]", Name = "LogOut")]
        // public async Task<IActionResult> Logout(string? ReturnUrl)
        // {
        //     await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //     return base.RedirectToRoute("LoginView", new { ReturnUrl });
        // }


        // [HttpGet]
        // [Route("[controller]/[action]", Name = "ConfirmationView")]
        // public async Task<IActionResult> Confirmation(){
        //     return base.View();
        // }

        
    }
}
