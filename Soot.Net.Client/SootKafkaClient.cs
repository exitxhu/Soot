using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Soot.Application.Command;
using Soot.Common.Models;
using Soot.Models;
using System.Net;

namespace Soot.Net.Client
{
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
        public async Task SendForceSmsAsync(SendLookupSmsModel model)
        {
            var streamModel = new DispatchableJobArgument(JobIdConstants.LookupSms, model);
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