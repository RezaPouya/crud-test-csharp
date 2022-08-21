using FluentValidation;
using Mc2.CrudTest.Utility.Helpers;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .MaximumLength(254)
                .Must(email => EmailHelper.IsValidEmail(email))
                .WithMessage("Email is not valid.");
        }
    }
}