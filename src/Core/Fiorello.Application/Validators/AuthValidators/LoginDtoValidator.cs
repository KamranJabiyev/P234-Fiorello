using Fiorello.Application.DTOs.AuthDtos;
using FluentValidation;

namespace Fiorello.Application.Validators.AuthValidators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100);
        RuleFor(u => u.Username)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);
    }
}
