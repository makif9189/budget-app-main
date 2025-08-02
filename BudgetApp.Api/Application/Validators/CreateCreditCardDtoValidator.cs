using FluentValidation;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Constants;

namespace BudgetApp.Api.Application.Validators;

public class CreateCreditCardDtoValidator : AbstractValidator<CreateCreditCardDto>
{
    public CreateCreditCardDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Credit card name is required.")
            .Length(3, ApplicationConstants.Validation.MaxNameLength)
            .WithMessage($"Name must be between 3 and {ApplicationConstants.Validation.MaxNameLength} characters.");

        RuleFor(x => x.BankName)
            .MaximumLength(ApplicationConstants.Validation.MaxNameLength)
            .WithMessage($"Bank name must not exceed {ApplicationConstants.Validation.MaxNameLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.BankName));

        RuleFor(x => x.Last4Digits)
            .Matches(ApplicationConstants.Patterns.Last4Digits)
            .WithMessage("Last 4 digits must be exactly 4 numbers.")
            .When(x => !string.IsNullOrEmpty(x.Last4Digits));

        RuleFor(x => x.StatementDay)
            .InclusiveBetween(ApplicationConstants.Validation.MinStatementDay, 
                             ApplicationConstants.Validation.MaxStatementDay)
            .WithMessage($"Statement day must be between {ApplicationConstants.Validation.MinStatementDay} and {ApplicationConstants.Validation.MaxStatementDay}.");

        RuleFor(x => x.PaymentDueDateOffset)
            .InclusiveBetween(ApplicationConstants.Validation.MinPaymentOffset, 
                             ApplicationConstants.Validation.MaxPaymentOffset)
            .WithMessage($"Payment due date offset must be between {ApplicationConstants.Validation.MinPaymentOffset} and {ApplicationConstants.Validation.MaxPaymentOffset} days.");

        RuleFor(x => x.CardLimit)
            .GreaterThan(0).WithMessage("Card limit must be greater than 0.")
            .When(x => x.CardLimit.HasValue);

        RuleFor(x => x.ExpirationDate)
            .Matches(ApplicationConstants.Patterns.ExpirationDate)
            .WithMessage("Expiration date must be in MM/YY format.")
            .Must(BeValidExpirationDate).WithMessage("Expiration date must be in the future.")
            .When(x => !string.IsNullOrEmpty(x.ExpirationDate));
    }

    private static bool BeValidExpirationDate(string? expirationDate)
    {
        if (string.IsNullOrEmpty(expirationDate)) return true;

        try
        {
            var parts = expirationDate.Split('/');
            if (parts.Length != 2) return false;

            var month = int.Parse(parts[0]);
            var year = 2000 + int.Parse(parts[1]); // Convert YY to YYYY

            var expiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return expiryDate > DateTime.Now;
        }
        catch
        {
            return false;
        }
    }
}