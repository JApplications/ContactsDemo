using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsDemo.Api.Hubs;
using ContactsDemo.Services;
using ContactsDemo.Services.Interfaces;
using ContactsDemo.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ContactsDemo
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
            services.AddControllers();
            services.AddMvc();
            services.AddDbContext<ContactsDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactNumberService, ContactNumberService>();

            services.AddCors(o => o.AddPolicy("AllPolicy", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins("https://localhost:44345", "http://localhost:44345");
                builder.AllowCredentials();
            }));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ContactHub>("/contact-hub");
            });
        }
    }
}
