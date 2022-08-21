using Mc2.CrudTest.Domain.Application.Customers.Queries;
using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using Mc2.CrudTest.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Domain.Application.Customers.QueryHandlers
{
    public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, CustomerOutputDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCustomerByEmailQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerOutputDto> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                return null;

            return await _dbContext.Customers.AsNoTracking()
                .Where(p => p.Email.Equals(request.Email.ToString()))
                .Select(p => new CustomerOutputDto
                {
                    BankAccountNumber = p.BankAccountNumber,
                    DateOfBirth = p.PersonalInfo.DateOfBirth,
                    Email = p.Email,
                    FirstName = p.PersonalInfo.FirstName,
                    LastName = p.PersonalInfo.LastName,
                    PhoneNumber = p.PhoneNumber.Number
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}