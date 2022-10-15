using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Soot.Application.Command;
using Soot.Common.Models;
using Soot.Domain.Shared;
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
        public async Task<ResultDto?> SendLookupSmsAsync(SendLookupSmsModel model)
        {
            var msg = JsonContent.Create(model);
            var resp = await _httpClient.PostAsync(new Uri(_config.HostUri + "/LookupSms"), msg);
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
}