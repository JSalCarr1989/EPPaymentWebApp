using EPPCIDAL.Services;
using Microsoft.Extensions.DependencyInjection;
using EPPCIDAL.Interfaces;
using EPPCIDAL.Repositories;

namespace EPPaymentWebApp.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddTransient<IRequestPaymentRepository, RequestPaymentRepository>();
            services.AddTransient<IBeginPaymentRepository, BeginPaymentRepository>();
            services.AddTransient<IEndPaymentRepository, EndPaymentRepository>();
            services.AddTransient<IResponsePaymentRepository, ResponsePaymentRepository>();
            services.AddTransient<ILogPaymentRepository, LogPaymentRepository>();
            services.AddTransient<IDbLoggerErrorRepository, DbLoggerErrorRepository>();
            services.AddTransient<IHashRepository, HashRepository>();
            services.AddTransient<IEnvironmentSettingsRepository, EnvironmentSettingsRepository>();
            services.AddTransient<IDbConnectionRepository, DbConnectionRepository>();
            services.AddTransient<IDbLoggerConfigurationRepository, DbLoggerConfigurationRepository>();
            services.AddTransient<IDbLoggerRepository, DbLoggerRepository>();
            services.AddTransient<IDbConsoleLoggerRepository, DbConsoleLoggerRepository>();
            services.AddTransient<IDbConsoleLoggerErrorRepository, DbConsoleLoggerErrorRepository>();
            services.AddTransient<IResponsePaymentService, ResponsePaymentService>();
            services.AddTransient<IEndPaymentService, EndPaymentService>();
            services.AddTransient<IRequestPaymentService, RequestPaymentService>();



            return services;
        }
    }
}
