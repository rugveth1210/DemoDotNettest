using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EmployeeApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace EmployeeApi
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

                     services.AddHealthChecks();
                     services.AddAuthorization();
                     services.AddControllers();
                     services.AddMvc().AddControllersAsServices();

        // services.AddCors(options =>
        // {
        //     options.AddPolicy("CorsApi",
        //         builder => builder.WithOrigins("https://localhost:5001/api/employeeitems","http://localhost:4200")
        //             .AllowAnyHeader()
        //             .AllowAnyMethod()
        //             .AllowAnyOrigin());
        // });
        services.AddDbContext<EmployeeContext>(options => options.UseSqlServer (Configuration.GetConnectionString("DefaultConnection"), options2 => options2.EnableRetryOnFailure()));

            // services.AddControllers();
            // services.AddDbContext<EmployeeContext>(opt =>
            //                                    opt.UseInMemoryDatabase("EmployeeList"));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeApi", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
   

//cors configuration. With the cors filter removed we can connect to other origin points (angular, etc)
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseHttpsRedirection(); 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //swagger is available to test the APIs, but comment this out so you could use rest client or postman instead
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeApi v1"));
            }

//enable routing and http redirection
            app.UseHttpsRedirection();

            app.UseRouting();
            //authorization must be enabled with regards to sql server from my understanding

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

                            // NOTE: this must go at the end of Configure

        //the ensurecreated function ensures that before building and running the project, the database is created. If it isn't created, it creates it.
        var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<EmployeeContext>();
            dbContext.Database.EnsureCreated();

        }
        }
    }
}
