using Kavenegar_NetCore_unofficial_;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soot.Application.Module.Sms;

namespace Soot.Sms.Kavenegar
{
    public static class Helpers
    {
        public static IServiceCollection AddSootSmsKavenegar(this IServiceCollection services, IConfigurationSection kavenegarSection)
            => services.AddKavenegar(kavenegarSection)
            .AddScoped<ISmsModule, KavenegarSmsModule>();
    }
}
