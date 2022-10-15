using linqPlusPlus;
using Soot.Application.Command;
using Soot.Models;

namespace Soot.Rest.Events
{
    public class LookupSms : IDispatchableJob
    {
        ISendLookupSms sms;

        public LookupSms(ISendLookupSms sms)
        {
            this.sms = sms;
        }

        public string JobId => JobIdConstants.LookupSms;

        public async Task Dispatch(DispatchableJobArgument argument)
        {
            var model = argument.ArgumentObject.Map<SendLookupSmsModel>();
            if (model?.IsValid != true)
                throw new ArgumentException($"invalid argument: {model.ToJson()}");
            sms.Model = model;
            await sms.ExecuteAsync();
        }
    }
}
