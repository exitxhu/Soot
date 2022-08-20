using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soot.Application.Command;
using Soot.Common.Models;
using Soot.Domain.Shared;
using Soot.Models;
using System.Net;
using System.Net.Http.Json;

namespace Soot.Net.Client
{
    public class SootRestClient
    {
        private readonly SootRestConfig _config;
        private readonly HttpClient _httpClient;

        public SootRestClient(HttpClient httpClient, IOptions<SootRestConfig> options)
        {
            _config = options.Value;
            this._httpClient = httpClient;
        }
        public async Task<ResultDto?> SendForceSmsAsync(SendSmsModel model)
        {
            var msg = JsonContent.Create(model);
            var resp = await _httpClient.PostAsync(new Uri(_config.HostUri + "/ForceSms"), msg);
            var res = JsonConvert.DeserializeObject<ResultDto>(await resp.Content.ReadAsStringAsync());
            return res;
        }
        public async Task<ResultDto?> SendForceEmailAsync(SendMailModel model)
        {
            var msg = JsonContent.Create(model);
            var resp = await _httpClient.PostAsync(new Uri(_config.HostUri + "/ForceEmail"), msg);
            var res = JsonConvert.DeserializeObject<ResultDto>(await resp.Content.ReadAsStringAsync());
            return res;
        }
    }
    public class SootKafkaClient
    {
        private readonly ProducerConfig _config;
        private readonly SootKafkaConfig _options;

        public SootKafkaClient(IOptions<SootKafkaConfig> options)
        {
            _options = options.Value;
            _config = new ProducerConfig
            {
                BootstrapServers = _options?.Server,
                ClientId = Dns.GetHostName(),
            };
        }
        public async Task SendForceSmsAsync(SendSmsModel model)
        {
            var streamModel = new DispatchableJobArgument(JobIdConstants.ForceSms, model);
            var msg = streamModel.ToJson();
            await ProduceAsync(msg);
        }
        public async Task SendForceEmailAsync(SendMailModel model)
        {
            var streamModel = new DispatchableJobArgument(JobIdConstants.ForceEmail, model);
            var msg = streamModel.ToJson();
            await ProduceAsync(msg);
        }
        async Task ProduceAsync(string message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            foreach (var topic in _options.Topics)
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}