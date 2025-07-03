global using LogisticsSolution.Infrastructure;
using LogisticsSolution.Application.BusinessLogic;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Contract.ExternalServices;
using LogisticsSolution.Application.Contract.Notification;
using LogisticsSolution.Infrastructure.ExternalServices;
using LogisticsSolution.Infrastructure.Notification;
using LogisticsSolution.Infrastructure.Persistance;

namespace LogisticsSolution.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void InjectRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void InjectService(this IServiceCollection services)
        {
            services.AddScoped<IApiRequestHandler, HttpClientApiRequestHandler>();
            services.AddScoped<INotification, NotificationService>();
            services.AddSignalR();
            services.AddMemoryCache();
            services.AddScoped<ICache, CacheService>();
            services.AddScoped<IProvince, ProvinceService>();
            services.AddScoped<IMove, MoveService>();
            services.AddScoped<IAnalyser, AnalyserService>();
            services.AddScoped<IAgent, AgentsService>();
            services.AddScoped<IKvk, KvkService>();
            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<IQuote, QuoteService>();
            services.AddScoped<IMailing, MailingService>();
        }
    }
}
