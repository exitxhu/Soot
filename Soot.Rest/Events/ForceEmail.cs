using linqPlusPlus;
using Soot.Application.Command;
using Soot.Models;

namespace Soot.Rest.Events
{
    public class ForceEmail : IDispatchableJob
    {
        ISendForceEmail email;

        public ForceEmail(ISendForceEmail email)
        {
            this.email = email;
        }

        public string JobId => JobIdConstants.ForceEmail;

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
