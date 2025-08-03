using FluentValidation;
using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Api.Validators;
public class CreateExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
{
    public CreateExpenseDtoValidator()
    {
        RuleFor(x => x.ExpenseCategoryId).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.TransactionDate).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
