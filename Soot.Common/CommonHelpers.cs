using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soot.Application.Helpers;
using Soot.Application.Module.Email;
using Soot.Application.Module.Sms;
using Soot.Db.Ef.Helpers;
using Soot.Mail.Mailkit;
using Soot.Sms.Kavenegar;

namespace Soot.Common
{
    public static class CommonHelpers
    {
        public static IServiceCollection AddEdgeLayerCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            var smtp = configuration.GetSection("SmtpConfig");
            services.Configure<MailConfiguration>(smtp);
            smtp.Get<MailConfiguration>();
            services.AddScoped<IEmailModule, MailkitEmailModule>();
            services.AddScoped<ISmsModule, KavenegarSmsModule>();
            services.AddSootDbEf(a => a.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddSootApplication();
            services.AddSootSmsKavenegar(configuration.GetSection("KavenegarConfig"));
            return services;
        }
    }
}