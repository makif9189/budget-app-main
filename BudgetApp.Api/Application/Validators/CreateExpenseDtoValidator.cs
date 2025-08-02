using FluentValidation;
using BudgetApp.Api.Core.DTOs;
using BudgetApp.Api.Core.Constants;

namespace BudgetApp.Api.Application.Validators;

public class CreateExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
{
    public CreateExpenseDtoValidator()
    {
        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0).WithMessage("Expense category is required.");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(ApplicationConstants.Validation.MinAmount)
            .WithMessage($"Amount must be at least {ApplicationConstants.Validation.MinAmount}.");

        RuleFor(x => x.TransactionDate)
            .NotEmpty().WithMessage("Transaction date is required.")
            .LessThanOrEqualTo(DateTime.Today.AddDays(1))
            .WithMessage("Transaction date cannot be in the future.");

        RuleFor(x => x.Description)
            .MaximumLength(ApplicationConstants.Validation.MaxDescriptionLength)
            .WithMessage($"Description must not exceed {ApplicationConstants.Validation.MaxDescriptionLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}