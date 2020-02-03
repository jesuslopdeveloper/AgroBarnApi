using AgroBarn.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace AgroBarn.API.Configurations
{
    public static class ConfigureConnections
    {
         public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = configuration.GetConnectionString("AgroBarnDbWindows") ?? 
                            "Server=localhost;Database=AgroBarn;User=sa;Password=siavilla;Trusted_Connection=True;Application Name=AgroBarnApi";
            }

            services.AddDbContext<AgroBarnContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
