using BlogApp.Core.Dtos;
using BlogApp.Core.Models;
using BlogApp.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers
{
    public class IdentityController : Controller
    {
        public IUserService userService;
        private readonly IDataProtector dataProtector;
        private readonly IValidator<RegistrationDto> userValidator;

        public IdentityController(IValidator<RegistrationDto> userValidator, IUserService userService, IDataProtectionProvider dataProtectionProvider)
        {
            this.userService = userService;
            this.dataProtector = dataProtectionProvider.CreateProtector("identity");
            this.userValidator = userValidator;
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


        [Route("api/[controller]/[action]", Name = "LoginEndPoint")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            var foundUser = await userService.GetSignedUpUser(loginDto);
            if (foundUser == null || foundUser.Email != loginDto.Email)
            {
                base.TempData["error"] = "Incorrect login or password!";
                return base.RedirectToRoute("LoginView");
            }



            return base.RedirectToAction(controllerName: "Home", actionName: "Index");
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
                        System.Console.WriteLine(error.ErrorMessage);
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View();
                }


                await this.userService.CreateAsync(registrationDto);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return base.RedirectToRoute("RegistrationView");
            }

            return base.RedirectToRoute("LoginView");
        }

        [HttpGet]
        [Route("/api/[controller]/[action]", Name = "LogOut")]
        public async Task<IActionResult> Logout(string? ReturnUrl)
        {

            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return base.RedirectToRoute("LoginView", new
            {
                ReturnUrl
            });
        }

    }
}