using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Application.Common.Behaviors.FluentValidation.Users;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(u => u.Email)
            .EmailAddress()
            .MinimumLength(7).WithMessage("Email Most be more then 7 characters")
            .NotEmpty()
            .NotNull()
            .NotEqual("admin@gmail.com").WithMessage("You can't use this Email address")
            .NotEqual("administrator@gmail.com").WithMessage("You can't use this Email address")
            .MaximumLength(100).WithMessage("Email Most be less then 100 characters");

        RuleFor(u => u.Password)
            .MinimumLength(8).WithMessage("Password Most be more then 8 characters")
            .NotEmpty()
            .NotNull()
            .NotEqual("admin").WithMessage("This password is not correct")
            .NotEqual("administrator").WithMessage("This password is not correct")
            .MaximumLength(128).WithMessage("Password Most be less then 128 characters");
    }
}
