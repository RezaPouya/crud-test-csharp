using FluentValidation;
using Mc2.CrudTest.Domain.Application;
using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.DataAccess;
using Mc2.CrudTest.Domain.Manager.Customers;
using Mc2.CrudTest.WebUi.Behaviours;
using Mc2.CrudTest.WebUi.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Mc2.CrudTest.WebUi.Configurations
{
    internal static class ServiceConfiguration
    {
        internal static void ConfigureServices(this WebApplicationBuilder? builder)
        {
            if (builder is null)
                throw new NullReferenceException("WebApplicationBuilder is null");

            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddMediatR(typeof(DomainApplicationAssemblyMarker).GetTypeInfo().Assembly);
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //builder.Services.AddControllers(
            //    options =>
            //    {
            //        options.Filters.Add<ApiExceptionFilterAttribute>();
            //        options.Filters.Add<ApiResultFilterAttribute>();
            //    });

            //builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>());
            //builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCustomerCommandValidator));
            //builder.Services.AddValidatorsFromAssembly(typeof(DomainApplicationAssemblyMarker).Assembly);

            builder.Services.AddCustomServices(configuration);
        }

        private static void AddCustomServices(this IServiceCollection services, ConfigurationManager? configurationManager)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
        }
    }
}