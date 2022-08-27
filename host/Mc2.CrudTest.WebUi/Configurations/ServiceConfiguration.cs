using Mc2.CrudTest.Domain.Application;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.DataAccess;
using Mc2.CrudTest.Domain.Manager.Customers;
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

            // https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient);

            builder.Services.AddMediatR(typeof(DomainApplicationAssemblyMarker).GetTypeInfo().Assembly);

            builder.Services.AddCustomServices(configuration);
        }

        private static void AddCustomServices(this IServiceCollection services, ConfigurationManager? configurationManager)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
        }
    }
}