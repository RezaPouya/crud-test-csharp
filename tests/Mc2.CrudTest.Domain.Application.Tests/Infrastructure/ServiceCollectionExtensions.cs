﻿using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Mc2.CrudTest.Domain.Application.Tests.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Remove<TService>(this IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(TService));

            if (serviceDescriptor != null)
            {
                services.Remove(serviceDescriptor);
            }

            return services;
        }
    }
}