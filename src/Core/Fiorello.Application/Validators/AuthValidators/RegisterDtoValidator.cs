using Fiorello.Application.DTOs.AuthDtos;
using FluentValidation;

namespace Fiorello.Application.Validators.AuthValidators;

public class RegisterDtoValidator:AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(u => u.Fullname)
            .MaximumLength(150);
        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100);
        RuleFor(u => u.Email)
            .EmailAddress()
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(u=>u.Username)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);
    }
}
