using FluentValidation;
using BudgetApp.Api.Core.DTOs;

namespace BudgetApp.Api.Api.Validators;
public class CreateCreditCardDtoValidator : AbstractValidator<CreateCreditCardDto>
{
    public CreateCreditCardDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 100);
        RuleFor(x => x.BankName).MaximumLength(100);
        RuleFor(x => x.Last4Digits)
            .Matches(@"^\d{4}$")
            .When(x => !string.IsNullOrEmpty(x.Last4Digits));
        RuleFor(x => x.StatementDay).InclusiveBetween(1, 31);
        RuleFor(x => x.PaymentDueDateOffset).InclusiveBetween(0, 30);
        RuleFor(x => x.CardLimit)
            .GreaterThanOrEqualTo(0)
            .When(x => x.CardLimit.HasValue);
        RuleFor(x => x.ExpirationDate)
            .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$")
            .When(x => !string.IsNullOrEmpty(x.ExpirationDate));
    }
}
