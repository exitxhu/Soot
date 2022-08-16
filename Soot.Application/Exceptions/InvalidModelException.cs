using System.Runtime.Serialization;
using Soot.Application.Base;

namespace Soot.Application.Exceptions
{
    [Serializable]
    internal class InvalidModelException : Exception
    {
        public InvalidModelException()
        {
        }

        public InvalidModelException(string? message) : base(message)
        {
        }

        public InvalidModelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public static void ThrowIfInvalid(ModelBase model)
        {
            if (model is null || !model.IsValid)
                throw new InvalidModelException($"Model {model?.GetType().Name} is not valid");
        }
        public static void ThrowIfInvalid(IEnumerable<ModelBase> model)
        {
            if (model is null || model.Any(n => !n.IsValid))
                throw new InvalidModelException($"List of models {model?.GetType().Name} contains atleast one invalid model");
        }
    }
}
