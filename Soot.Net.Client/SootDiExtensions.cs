using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soot.Net.Client
{
    public static class SootDiExtensions
    {
        public static IServiceCollection AddSoot(this IServiceCollection services, IConfigurationSection section)
        {
            var config = section.Get<SootConfig>();
            services.Configure<SootConfig>(section);
            services.AddHttpClient<SootClient>(a => a.BaseAddress = config.HostUri);

            return services;
        }
        public static IServiceCollection AddSoot(this IServiceCollection services, Action<SootConfig> sootConfig)
        {

            var config = new SootConfig();
            sootConfig(config);
            services.Configure<SootConfig>(sootConfig);
            services.AddHttpClient<SootClient>(a => a.BaseAddress = config.HostUri);

            return services;
        }
    }
}