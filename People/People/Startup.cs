using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using People.ModelsDB;
using People.Repositories;
using People.WebControllers;
using Serilog;

namespace People
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "People", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                
            });
            services.AddSwaggerGenNewtonsoftSupport();

            AddDbContext(services, Configuration);
            AddLogging(services, Configuration);
            
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<PersonController>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "People v1"));

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PersonContext>(opt => 
                opt.UseNpgsql(config.GetConnectionString("Heroku")));
        }
        
        private static void AddLogging(IServiceCollection services, IConfiguration config)
        {
            var log = new LoggerConfiguration()
                .WriteTo.File(config["Logger"])
                .CreateLogger();
            
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(logger: log, dispose: true));
        }
    }
}
