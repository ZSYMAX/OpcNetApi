using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class InvalidResponseException : ApplicationException
    {
        public InvalidResponseException() : base("The response from the server was invalid or incomplete.")
        {
        }

        public InvalidResponseException(string message) : base("The response from the server was invalid or incomplete.\r\n" + message)
        {
        }

        public InvalidResponseException(Exception e) : base("The response from the server was invalid or incomplete.", e)
        {
        }

        public InvalidResponseException(string message, Exception innerException) : base("The response from the server was invalid or incomplete.\r\n" + message, innerException)
        {
        }

        protected InvalidResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The response from the server was invalid or incomplete.";
    }
}