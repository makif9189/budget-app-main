using FluentValidation;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Constants;
using BudgetApp.Api.Core.Interfaces.Repositories;

namespace BudgetApp.Api.Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(ApplicationConstants.Authentication.MinUsernameLength, 
                   ApplicationConstants.Authentication.MaxUsernameLength)
            .WithMessage($"Username must be between {ApplicationConstants.Authentication.MinUsernameLength} and {ApplicationConstants.Authentication.MaxUsernameLength} characters.")
            .MustAsync(BeUniqueUsername).WithMessage("Username is already taken.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(ApplicationConstants.Validation.MaxEmailLength)
            .WithMessage($"Email must not exceed {ApplicationConstants.Validation.MaxEmailLength} characters.")
            .MustAsync(BeUniqueEmail).WithMessage("Email is already registered.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(ApplicationConstants.Authentication.MinPasswordLength, 
                   ApplicationConstants.Authentication.MaxPasswordLength)
            .WithMessage($"Password must be between {ApplicationConstants.Authentication.MinPasswordLength} and {ApplicationConstants.Authentication.MaxPasswordLength} characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, and one digit.");
    }

    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return !await _userRepository.ExistsByUsernameAsync(username);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return !await _userRepository.ExistsByEmailAsync(email);
    }
}