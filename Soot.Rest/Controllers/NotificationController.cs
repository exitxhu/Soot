using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soot.Application.Command;
using Soot.Domain;
using Soot.Domain.Shared;
using System.Text.Json.Serialization;

namespace Soot.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> logger;
        private readonly ISendForceEmail email;
        private readonly ISendForceSms sms;

        public NotificationController(ILogger<NotificationController> logger,
            ISendForceEmail email,
            ISendForceSms sms)
        {
            this.sms = sms;
            this.logger = logger;
            this.email = email;
        }
        [HttpPost("[action]")]
        public async Task SceduleNotification()
        {

        }
        [HttpPost("[action]")]
        public async Task<ResultDto> ForceEmail(SendMailModel model)
        {
            email.Model = model;
            var res = await email.ExecuteAsync();
            return res;
        }
        [HttpPost("[action]")]
        public async Task<ResultDto> ForceSms(SendSmsModel model, [FromServices] ISendForceSms sms)
        {
            sms.Model = model;
            var res = await sms.ExecuteAsync();
            return res;
        }
    }

}
