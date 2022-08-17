using Mc2.CrudTest.HttpApi.Host;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mc2.CrudTest.Domain.Application.Tests
{
    public class ApplicationTestFixture : IDisposable
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static IConfiguration _configuration = null!;
        private static IServiceScopeFactory _scopeFactory = null!;
        private static IMediator _mediator;

        public ApplicationTestFixture()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();

            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}