
using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Models;
using FluentValidation;


namespace BlogApp.Presentation.Validators;


public class UserRegistrationValidator : AbstractValidator<RegistrationDto>
{
     public UserRegistrationValidator()
    {


        base.RuleFor(u => u.Email)
                        .NotEmpty()
                        .EmailAddress();

        


        base.RuleFor(u => u.Name)
            .NotEmpty();

        


    }

        
}
