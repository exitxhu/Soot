using System.Runtime.Serialization;

namespace Soot.Domain
{
    [Serializable]
    internal class InvalidEmailAddressFormatException : Exception
    {
        public InvalidEmailAddressFormatException()
        {
        }

        public InvalidEmailAddressFormatException(string? message) : base(message)
        {
        }

        public InvalidEmailAddressFormatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidEmailAddressFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}