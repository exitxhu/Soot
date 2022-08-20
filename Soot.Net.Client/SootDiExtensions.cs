using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soot.Common.Models;

namespace Soot.Net.Client
{
    public static class SootDiExtensions
    {
        public static IServiceCollection AddSootRestClient(this IServiceCollection services, IConfigurationSection section)
        {
            var config = section.Get<SootRestConfig>();
            services.Configure<SootRestConfig>(section);
            services.AddHttpClient<SootRestClient>(a => a.BaseAddress = config.HostUri);
            return services;
        }
        public static IServiceCollection AddSootRestClient(this IServiceCollection services, Action<SootRestConfig> sootConfig)
        {
            var config = new SootRestConfig();
            sootConfig(config);
            services.Configure(sootConfig);
            services.AddHttpClient<SootRestClient>(a => a.BaseAddress = config.HostUri);
            return services;
        }
        public static IServiceCollection AddSootKafkaClient(this IServiceCollection services, IConfigurationSection section)
        {
            var config = section.Get<SootRestConfig>();
            services.Configure<SootRestConfig>(section);
            services.AddTransient<SootKafkaClient>();
            return services;
        }
        public static IServiceCollection AddSootKafkaClient(this IServiceCollection services, Action<SootKafkaConfig> sootConfig)
        {
            var config = new SootKafkaConfig();
            sootConfig(config);
            services.Configure(sootConfig);
            services.AddTransient<SootKafkaClient>();
            return services;
        }
    }
}