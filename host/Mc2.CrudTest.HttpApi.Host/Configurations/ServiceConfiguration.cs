using FluentValidation;
using FluentValidation.AspNetCore;
using Mc2.CrudTest.Domain.Application;
using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.DataAccess;
using Mc2.CrudTest.Domain.Manager.Customers;
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

            // Add services to the container.
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(typeof(DomainApplicationAssemblyMarker).GetTypeInfo().Assembly);

            AddControllerWithFluentValidation(builder);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCustomServices(configuration);
        }

        private static void AddControllerWithFluentValidation( WebApplicationBuilder? builder)
        {
            // TODO : solve the issue , although this is obsolete  , but it work 
            builder.Services.AddControllers().AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;
                options.RegisterValidatorsFromAssembly(typeof(DomainApplicationAssemblyMarker).GetTypeInfo().Assembly);
            }); ;

            //builder.Services.AddControllers();
            //builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCustomerCommandValidator));
            //builder.Services.AddValidatorsFromAssembly(typeof(DomainApplicationAssemblyMarker).Assembly);
        }

        private static void AddCustomServices(this IServiceCollection services, ConfigurationManager? configurationManager)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
        }
    }
}