using System;
using System.Runtime.Serialization;

namespace Opc
{
    public class ServerTimeoutException : ResultIDException
    {
        public ServerTimeoutException() : base(ResultID.E_TIMEDOUT, "The server did not respond within the specified timeout period.")
        {
        }

        public ServerTimeoutException(string message) : base(ResultID.E_TIMEDOUT, "The server did not respond within the specified timeout period.\r\n" + message)
        {
        }

        public ServerTimeoutException(Exception e) : base(ResultID.E_TIMEDOUT, "The server did not respond within the specified timeout period.", e)
        {
        }

        public ServerTimeoutException(string message, Exception innerException) : base(ResultID.E_TIMEDOUT, "The server did not respond within the specified timeout period.\r\n" + message, innerException)
        {
        }

        protected ServerTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The server did not respond within the specified timeout period.";
    }
}