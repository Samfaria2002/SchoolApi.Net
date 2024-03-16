using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UdemyApiDotNet.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace UdemyApiDotNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(
                context => context.UseMySql(Configuration.GetConnectionString("MySqlConnection"), providerOptions => providerOptions.EnableRetryOnFailure())
            );

            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            services.AddSwaggerGen(options =>{
                options.SwaggerDoc(
                    "udemyapi",
                    new Microsoft.OpenApi.Models.OpenApiInfo() {
                        Title = "UdemyApi",
                        Version = "1.0",
                        TermsOfService = new Uri("http://ApenasParaExemplo.com"),
                        Description = "Uma web api feita usando .NetCore",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense{
                            Name = "Samuel .NetCore Api License",
                            Url = new Uri("http://mit.com")
                        },
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact{
                            Name = "Samuel Faria Garcia",
                            Email = "samuel.faria2002@hotmail.com"
                        }
                    }
                );

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);


                options.IncludeXmlComments(xmlCommentsFullPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger().UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/udemyapi/swagger.json", "udemyapi");
                options.RoutePrefix = "";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
