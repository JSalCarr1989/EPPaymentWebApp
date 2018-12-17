using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EPPaymentWebApp.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddTransient<IDbConnectionRepository, DbConnectionRepository>();
            services.AddTransient<IDbLoggerRepository, DbLoggerRepository>();
            services.AddTransient<IBeginPaymentRepository, BeginPaymentRepository>();
            services.AddTransient<IResponsePaymentRepository, ResponsePaymentRepository>();
            services.AddTransient<ILogPaymentRepository, LogPaymentRepository>();
            services.AddTransient<IEndPaymentRepository, EndPaymentRepository>();
            services.AddTransient<IResponseBankRequestTypeTibcoRepository, ResponseBankRequestTypeTibcoRepository>();
            services.AddTransient<IEnterprisePaymentViewModelRepository, EnterprisePaymentViewModelRepository>();
            services.AddTransient<ISentToTibcoRepository, SentToTibcoRepository>();

            return services;
        }
    }
}
