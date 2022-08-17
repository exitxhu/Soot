using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Soot.Domain.Shared;
using Soot.Application.Module.Email;
using Soot.Domain.Entities;

namespace Soot.Mail.Mailkit
{
    public class MailkitEmailModule : IEmailModule
    {
        private readonly MailConfiguration _smtpConfig;
        private readonly ILogger<MailkitEmailModule> _logger;
        private readonly MailboxAddress _from;

        public MailkitEmailModule(IOptions<MailConfiguration> smtpConfig, ILogger<MailkitEmailModule> logger)
        {
            this._smtpConfig = smtpConfig.Value;
            this._logger = logger;
            _from = new MailboxAddress(_smtpConfig.FromName, _smtpConfig.FromMail);
        }
        public async Task<ResultDto> SendAsync(Notification notification)
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, _smtpConfig.SecureSocket);
                await client.AuthenticateAsync(_smtpConfig.Username, _smtpConfig.Password);
                var msg = new MimeMessage();
                var builder = new BodyBuilder
                {
                    HtmlBody = notification.Body
                };
                msg.Body = builder.ToMessageBody();
                //msg.To.Add(new MailboxAddress(notification.Receiver.Name, notification.Receiver.EmailAddress.ToString()));
                msg.To.Add(_from);
                var res = await client.SendAsync(msg);
                await client.DisconnectAsync(true);
                return new ResultDto
                {
                    IsSuccessful = true,
                    Message = res
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error has occurred while sending mail");
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultDto> SendEmailAsync(string subject, string body, Contact receiver)
        {
            try
            {
                if (receiver?.EmailAddress is null) throw new InvalidDataException("email not found");
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, _smtpConfig.SecureSocket);
                await client.AuthenticateAsync(_smtpConfig.Username, _smtpConfig.Password);
                var msg = new MimeMessage();
                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };
                msg.Subject = subject;
                msg.Body = builder.ToMessageBody();
                msg.To.Add(new MailboxAddress("", receiver?.EmailAddress?.ToString()));
                msg.From.Add(_from);
                var res = await client.SendAsync(msg);
                await client.DisconnectAsync(true);
                return new ResultDto
                {
                    IsSuccessful = true,
                    Message = res
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error has occurred while sending mail");
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
