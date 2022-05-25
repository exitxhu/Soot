using System.Runtime.Serialization;

namespace Soot.Domain
{
    [Serializable]
    internal class InvalidMobileNumberFormatException : Exception
    {
        public InvalidMobileNumberFormatException()
        {
        }

        public InvalidMobileNumberFormatException(string? message) : base(message)
        {
        }

        public InvalidMobileNumberFormatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidMobileNumberFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}