using Mc2.CrudTest.Domain.Application.Customers.Queries;
using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using Mc2.CrudTest.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Domain.Application.Customers.QueryHandlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerOutputDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllCustomersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerOutputDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.AsNoTracking().Select(p => new CustomerOutputDto
            {
                BankAccountNumber = p.BankAccountNumber,
                DateOfBirth = p.PersonalInfo.DateOfBirth,
                Email = p.Email,
                FirstName = p.PersonalInfo.FirstName,
                LastName = p.PersonalInfo.LastName,
                PhoneNumber = p.PhoneNumber.Number
            }).ToListAsync(cancellationToken);
        }
    }
}