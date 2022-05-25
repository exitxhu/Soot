using Soot.Domain;
using Soot.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Application.Email
{
    public interface IEmailModule : ISoot
    {
        Task<ResultDto> SendEmailAsync(string Body, Contact Receiver);
    }
}
