using FluentValidation;
using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Api.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().Length(3, 50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty().Length(6, 100);
    }
}
