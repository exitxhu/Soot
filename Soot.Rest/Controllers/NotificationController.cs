using Microsoft.AspNetCore.Mvc;
using Soot.Application.Command;
using Soot.Domain.Shared;

namespace Soot.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly ISendForceEmail _email;
        private readonly ISendForceSms _sms;

        public NotificationController(ILogger<NotificationController> logger,
            ISendForceEmail email,
            ISendForceSms sms)
        {
            this._sms = sms;
            this._logger = logger;
            this._email = email;
        }
        [HttpPost("[action]")]
        public async Task ScheduleNotification()
        {

        }
        [HttpPost("[action]")]
        public async Task<ResultDto> ForceEmail(SendMailModel model)
        {
            _email.Model = model;
            var res = await _email.ExecuteAsync();
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
