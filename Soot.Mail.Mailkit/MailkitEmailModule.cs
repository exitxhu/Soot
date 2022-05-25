using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Soot.Application.Email;
using Soot.Domain;
using Soot.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Mail.Mailkit
{
    public class MailkitEmailModule : IEmailModule
    {
        private readonly MailConfiguration smtpConfig;
        private readonly ILogger<MailkitEmailModule> logger;
        private readonly MailboxAddress From = new MailboxAddress("Soot", "notif@soot.ir");

        public MailkitEmailModule(IOptions<MailConfiguration> smtpConfig, ILogger<MailkitEmailModule> logger)
        {
            this.smtpConfig = smtpConfig.Value;
            this.logger = logger;
        }
        public async Task<ResultDto> SendAsync(Notification notification)
        {
            try
            {
                using var client = new SmtpClient();
                client.Connect(smtpConfig.Host, smtpConfig.Port, smtpConfig.SSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None);

                client.Authenticate(smtpConfig.Username, smtpConfig.Password);

                var msg = new MimeMessage();
                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = notification.Body;
                msg.Body = builder.ToMessageBody();
                //msg.To.Add(new MailboxAddress(notification.Receiver.Name, notification.Receiver.EmailAddress.ToString()));
                msg.To.Add(From);
                var res = await client.SendAsync(msg);
                client.Disconnect(true);
                return new ResultDto
                {
                    IsSuccessful = true,
                    Message = res
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error has occured while sending mail");
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultDto> SendEmailAsync(string Body, Contact receiver)
        {
            try
            {
                using var client = new SmtpClient();
                client.Connect(smtpConfig.Host, smtpConfig.Port, smtpConfig.SSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None);

                client.Authenticate(smtpConfig.Username, smtpConfig.Password);

                var msg = new MimeMessage();
                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = Body;
                msg.Body = builder.ToMessageBody();
                msg.To.Add(new MailboxAddress("", receiver.EmailAddress.ToString()));
                msg.From.Add(From);
                var res = await client.SendAsync(msg);
                client.Disconnect(true);
                return new ResultDto
                {
                    IsSuccessful = true,
                    Message = res
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error has occured while sending mail");
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
