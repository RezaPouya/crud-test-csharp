using FluentValidation;
using Mc2.CrudTest.Utility.Helpers;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .MaximumLength(254)
                .Must(email => EmailHelper.IsValidEmail(email))
                .WithMessage("Email is not valid.");

            RuleFor(p => p.BankAccountNumber)
                .NotEmpty()
                .MaximumLength(34)
                .WithMessage("Bank account number is not valid.");

            RuleFor(p => p.DateOfBirth)
                .NotNull()
                .NotEqual(default(DateTime))
                .GreaterThan(DateTime.Now.AddYears(-150).Date)
                .LessThan(DateTime.Now)
                .WithMessage("Date of birth is not valid.");

            RuleFor(v => v.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(64)
                .WithMessage("First name is not valid.");

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(64)
                .WithMessage("Last name is not valid.");

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(64)
                .WithMessage("Last name is not valid.");

            RuleFor(v => v.PhoneNumber)
                .NotEmpty()
                .MaximumLength(31)
                .Must(email => PhoneNumberHelper.IsValidCellphoneNumber(email))
                .WithMessage("You should enter valid phone number.");
        }
    }
}