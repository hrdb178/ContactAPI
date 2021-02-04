using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DB.Repository;
using Microsoft.OpenApi.Models;

namespace Contact.API
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
            services.AddDbContext<ContactDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConnString")));

            services.AddScoped<ExceptionActionFilter>();
            services.AddMvc(options => options.Filters.AddService(typeof(ExceptionActionFilter)));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddSwaggerGen((option) =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact.API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
                               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact.API")
            );
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
