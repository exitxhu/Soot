using linqPlusPlus;
using Soot.Application.Command;
using Soot.Models;

namespace Soot.Kafka.Events
{
    public class ForceEmail : IDispatchableJob
    {
        ISendForceEmail email;

        public ForceEmail(ISendForceEmail email)
        {
            this.email = email;
        }

        public string JobId => JobIdConstants.ForceSms;

        public async Task Dispatch(DispatchableJobArgument argument)
        {
            var model = argument.ArgumentObject.Map<SendMailModel>();
            if (model?.IsValid != true)
                throw new ArgumentException($"invalid argument: {model.ToJson()}");
            email.Model = model;
            await email.ExecuteAsync();
        }
    }
}
