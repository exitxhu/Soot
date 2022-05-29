using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soot.Application.Command;
using Soot.Domain.Shared;
using System.Net.Http.Json;

namespace Soot.Net.Client
{
    public class SootClient
    {
        private SootConfig _config;
        private readonly HttpClient _httpClient;

        public SootClient(HttpClient httpClient, IOptions<SootConfig> options)
        {
            _config = options.Value;
            this._httpClient = httpClient;
        }
        public async Task<ResultDto> SendForceSmsAsync(IEnumerable<string> receptors, SendSmsModel model)
        {
            var msg = JsonContent.Create(model);
            var resp = await _httpClient.PostAsync(new Uri(_config.HostUri + "/ForceSms"), msg);
            var res = JsonConvert.DeserializeObject<ResultDto>( await resp.Content.ReadAsStringAsync());
            return res;
        }
        public async Task<ResultDto> SendForceEmailAsync(IEnumerable<string> receptors, SendMailModel model)
        {
            var msg = JsonContent.Create(model);
            var resp = await _httpClient.PostAsync(new Uri(_config.HostUri + "/ForceEmail"), msg);
            var res = JsonConvert.DeserializeObject<ResultDto>(await resp.Content.ReadAsStringAsync());
            return res;
        }
    }
    public class SootConfig
    {
        public string HostAddress { get; set; }
        public Uri HostUri => new Uri(HostAddress);

    }
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