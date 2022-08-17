using Microsoft.Extensions.DependencyInjection;
using Soot.Application.Command;

namespace Soot.Application.Helpers
{
    public static class DiExtension
    {
        public static IServiceCollection AddSootApplication(this IServiceCollection services)
        {
            //services.AddTransient<IEmailModule, Mail>();
            //services.AddTransient<ISendEmail, SendEmail>();
            //services.AddScoped();
            services.AddScoped<ISendForceEmail, SendForceEmail>();
            services.AddScoped<ISendForceSms, SendForceSms>();
            services.AddScoped<IUpsertContacts, UpsertContacts>();
            services.AddScoped<IDeleteContacts, DeleteContacts>();
            return services;
        }
    }
}
