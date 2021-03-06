﻿using AgroBarn.Domain.Supervisor.V1;
using AgroBarn.Domain.Repositories.V1;
using AgroBarn.Data.Repositories.V1;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AgroBarn.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            //Identity
            //People Repository
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            //Refresh Token Reposirory
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            //Catalogs
            //Message Repository
            services.AddScoped<IMessageRepository, MessageRepository>();
            //Breed Repository
            services.AddScoped<IBreedRepository, BreedRepository>();

            //Others TODO
            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<IAgroBarnSupervisor, AgroBarnSupervisor>();

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AnotherPolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://www.contoso.com")
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //        });

            //});

            return services;
        }
    }
}
