using Fiorello.Application.DTOs.CategoryDtos;
using FluentValidation;

namespace Fiorello.Application.Validators.CategoryValidators;

public class CategoryCrateDtoValidator: AbstractValidator<CategoryCreateDto>
{
    public CategoryCrateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().
            MaximumLength(100);
        RuleFor(x=>x.Description).
            MaximumLength(500);
    }
}
