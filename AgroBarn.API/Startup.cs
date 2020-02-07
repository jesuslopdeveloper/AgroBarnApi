using AgroBarn.API.Configurations;
using AgroBarn.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using AgroBarn.Domain.Repositories.V1;

namespace AgroBarn.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureRepositories()                    //Services
                .ConfigureSupervisor()                      //Supervisor
                .AddCorsConfiguration()                     //Cors
                .AddConnectionProvider(Configuration);      //Connection

            services.ConfigureSwagger();                    //Swagger configurations
            services.ConfigureAutoMapper();                 //AutoMapper
            services.ConfigureFluentValidation();           //FluentValidation

            services
                .ConfigureIdentity()                        //Identity
                .AddIdentityProvider(Configuration);        //Token Generation
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IPeopleRepository peopleRepository)
        {
            //Create Roles and SuperAdmin
            CreateRolesAndUser(services, peopleRepository).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "AgroBarn Api v1");
                config.RoutePrefix = string.Empty;     //Start Swagger UI in index page
            });
        }

        private async Task CreateRolesAndUser(IServiceProvider serviceProvider, IPeopleRepository peopleRepository)
        {
            //Adding cumtoms roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            //Type roles
            List<ApplicationRole> roles = new List<ApplicationRole>();
            roles.Add(new ApplicationRole
            {
                Name = "Admin",
                Description = "Rol de un Administrador",
                Status = 1,
            });

            roles.Add(new ApplicationRole
            {
                Name = "User",
                Description = "Rol de un usuario",
                Status = 1,
            });
            
            IdentityResult roleResult;

            foreach (var rol in roles)
            {
                //Creating the roles and saved in database
                var roleExist = await roleManager.RoleExistsAsync(rol.Name);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(rol);
                }
            }

            string email = Configuration.GetSection("SuperUserCredential")["Email"];
            string password = Configuration.GetSection("SuperUserCredential")["Password"];
            string name = Configuration.GetSection("SuperUserCredential")["Name"];
            string firstSurname = Configuration.GetSection("SuperUserCredential")["FirstSurname"];
            string secondSurname = Configuration.GetSection("SuperUserCredential")["SecondSurname"];

            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                var superUser = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    Status = 1
                };

                var createdUser = await userManager.CreateAsync(superUser, password);
                if (createdUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");

                    //Save data in people table
                    PeopleDto newPeople = new PeopleDto
                    {
                        UserId = superUser.Id,
                        Name = name,
                        FirstSurname = firstSurname,
                        SecondSurname = secondSurname,
                        Status = 1,
                        UserCreate = superUser.Id,
                        DateCreate = DateTime.Now
                    };

                    await peopleRepository.AddAsync(newPeople);
                }
            }
        }
    }
}
