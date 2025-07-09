using System;
using IdentityServer.Services;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddSingleton(new TestUserStore(Config.TestUsers));
           

            services.AddAuthentication();
            services.PostConfigure<CookieAuthenticationOptions>("idsrv", options =>
            {
                options.Cookie.SameSite = SameSiteMode.None; 
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
                options.Cookie.HttpOnly = true;
            });

            services.AddIdentityServer()
       .AddInMemoryClients(Config.Clients)
       .AddInMemoryIdentityResources(Config.IdentityResources)
       .AddInMemoryApiScopes(Config.ApiScopes)
       .AddProfileService<CustomProfileService>() 
       .AddTestUsers(Config.TestUsers)
       .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.ApiResources);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors();
            app.UseRouting();

          

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
