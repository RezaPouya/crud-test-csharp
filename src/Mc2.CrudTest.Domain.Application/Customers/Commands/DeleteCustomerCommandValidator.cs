using FluentValidation;
using Mc2.CrudTest.Domain.Helpers;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("Id is not valid.");
        }
    }
}