using FluentValidation;
using Mc2.CrudTest.Domain.Application;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.DataAccess;
using Mc2.CrudTest.Domain.Manager.Customers;
using Mc2.CrudTest.HttpApi.Host.Behaviours;
using Mc2.CrudTest.HttpApi.Host.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Mc2.CrudTest.HttpApi.Host.Configurations
{
    internal static class ServiceConfiguration
    {
        internal static void ConfigureServices(this WebApplicationBuilder? builder)
        {
            if (builder is null)
                throw new NullReferenceException("WebApplicationBuilder is null");

            var configuration = builder.Configuration;

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddMediatR(typeof(DomainApplicationAssemblyMarker).GetTypeInfo().Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            builder.Services.AddControllers(
                options =>
                {
                    options.Filters.Add<ApiExceptionFilterAttribute>();
                    options.Filters.Add<ApiResultFilterAttribute>();
                });

            builder.Services.AddValidatorsFromAssembly(typeof(DomainApplicationAssemblyMarker).Assembly);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCustomServices(configuration);
        }

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        private static void AddCustomServices(this IServiceCollection services, ConfigurationManager? configurationManager)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
        }
    }
}