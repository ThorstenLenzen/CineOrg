using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Toto.CineOrg.WebApi.Hosting
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGeneration(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CineOrg API", 
                    Version = "v1",
                    Description = "Totos playground example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Thorsten Lenzen",
                        Email = "thorsten.lenzen@outlook.com",
                        Url = new Uri("https://www.xing.com/profile/Thorsten_Lenzen/cv"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Just use it :-)",
                        Url = new Uri("https://nowhere.com/license"),
                    }
                });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerGeneration(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CineOrg API V1");
            });

            return app;
        }
    }
}