using AutoMapper;
using ConsumptionService.Automapper;
using ConsumptionService.Repository;
using ConsumptionService.Services;
using ConsumptionService.Settings;
using ConsumptionService.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ConsumptionService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.SettingsName));

            services.AddSingleton<IRepository, DevDataRepository>();
            services.AddTransient<IConsumptionCalculationService, ConsumptionCalculationService>();
            services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConsumptionModelProfile>();
            }).CreateMapper());
            services.AddScoped<IValidator<decimal>, InputValidator>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
