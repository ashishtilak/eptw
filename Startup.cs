using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePTW.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ePTW
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //TODO: Change this in case of development testing
            //ConnectionString = configuration.GetConnectionString("devConnection");
            ConnectionString = configuration.GetConnectionString("defaultConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(ConnectionString)
                );
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
