using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckJunkie.Model;
using FoodTruckJunkie.Repository;
using FoodTruckJunkie.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Serilog;
using Serilog.Core;

namespace FoodTruckJunkie.ApiServer
{
    public class Startup
    {
        private Serilog.ILogger _logger;
        private AppConfig _appconfig;

        public Startup(IConfiguration configuration)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .AddEnvironmentVariables()
                .AddUserSecrets("95e3d547-44ca-4e47-8bb8-226549098764");

            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CORS_Policy", builder =>
            {
                builder
                    .WithOrigins(
                        "https://localhost:5001",
                        "https://webapp-foodtruckjunkie-portal.azurewebsites.net"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            InitAppConfig();
            
            WireupDependencies(services);

            InitSerilog();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ErrorHandlingActionFilter(_logger));
            });

            services.AddApiVersioning(setup => {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true; //assume latest version. E.g 1.0, 1.1 - assumes 1.1
                setup.ReportApiVersions = true;
                setup.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddSwaggerGen();

            services.AddControllers();
        }

        private void InitAppConfig()
        {
            _appconfig = new AppConfig()
            {
                MySQLConnectionString = Configuration.GetValue<string>("MySQLConnectionStirng")
            };
        }

        private void WireupDependencies(IServiceCollection services)
        {
            InitSerilog();

            services.AddSingleton<IDbConnection>(sp => {
                return new MySqlConnection(_appconfig.MySQLConnectionString);
            });

            services.AddSingleton<AppConfig>( sp => {
                return _appconfig;
            });

            services.AddSingleton<ILogger>( sp => {
                return _logger;
            });
            
            services.AddTransient<IFoodTruckPermitService, FoodTruckPermitService>();
            services.AddTransient<IFoodTruckPermitRepository, FoodTruckPermitRepository>();
        }

        private void InitSerilog()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.MySQL(_appconfig.MySQLConnectionString, "App_Error_Logs", Serilog.Events.LogEventLevel.Error)
                .WriteTo.MySQL(_appconfig.MySQLConnectionString, "App_Info_Logs", Serilog.Events.LogEventLevel.Information)
                .CreateLogger();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            //serve swaggerui at root /
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseCors("CORS_Policy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
