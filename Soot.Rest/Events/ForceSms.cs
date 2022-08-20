using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using linqPlusPlus;
using Soot.Application.Command;
using Soot.Models;

namespace Soot.Rest.Events
{
    public class ForceSms : IDispatchableJob
    {
        ISendForceSms sms;

        public ForceSms(ISendForceSms sms)
        {
            this.sms = sms;
        }

        public string JobId => JobIdConstants.ForceSms;

        public async Task Dispatch(DispatchableJobArgument argument)
        {
            var model = argument.ArgumentObject.Map<SendSmsModel>();
            if (model?.IsValid != true)
                throw new ArgumentException($"invalid argument: {model.ToJson()}");
            sms.Model = model;
            await sms.ExecuteAsync();
        }
    }
}
