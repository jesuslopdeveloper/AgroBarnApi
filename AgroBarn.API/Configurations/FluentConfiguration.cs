using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.Validators.V1;

using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using AgroBarn.API.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;

namespace AgroBarn.API.Configurations
{
    public static class FluentConfiguration
    {
        public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        {
            //Disable default model validation
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services
                 .AddControllers(options =>
                 {
                     options.Filters.Add<ValidationFilter>();
                 })
                 .AddFluentValidation()
                 .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //--------------------------------------------------------------------------------------------------
            //Catalogs
            services.AddTransient<IValidator<BreedRequest>, BreedValidator>();

            return services;
        }
    }
}
