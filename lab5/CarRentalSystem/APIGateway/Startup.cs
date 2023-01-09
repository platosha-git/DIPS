using System.Reflection;
using APIGateway.AuthenticationMiddleware;
using APIGateway.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;

namespace APIGateway
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
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Gateway", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token based security",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, 
                        new string[] { }
                    }
                });
            });

            services.Configure<JwtConfiguration>(Configuration.GetSection("Jwt"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("JwtConfiguration:Issuer");
                    options.Audience = Configuration.GetValue<string>("JwtConfiguration:Audience");
                });
            services.AddAuthorization();
            
            services.AddScoped<IRentalsService, RentalsService>();
            services.AddControllers();
            
            services.Configure<CarsSettings>(Configuration.GetSection("CarsService"));
            services.Configure<PaymentsSettings>(Configuration.GetSection("PaymentsService"));
            services.Configure<RentalsSettings>(Configuration.GetSection("RentalsService"));
            
            AddHttpClients(services);
            AddLogging(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway v1"));

            app.UseRouting();
            
            app.UseMiddleware<AuthenticationMiddleware.AuthenticationMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void AddHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<ICarsRepository, CarsRepository>();
            services.AddHttpClient<IPaymentsRepository, PaymentsRepository>();
            services.AddHttpClient<IRentalsRepository, RentalsRepository>();
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
