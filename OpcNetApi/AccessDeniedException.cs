using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class AccessDeniedException : ResultIDException
    {
        public AccessDeniedException() : base(ResultID.E_ACCESS_DENIED, "The server refused the connection.")
        {
        }

        public AccessDeniedException(string message) : base(ResultID.E_ACCESS_DENIED, "The server refused the connection.\r\n" + message)
        {
        }

        public AccessDeniedException(Exception e) : base(ResultID.E_ACCESS_DENIED, "The server refused the connection.", e)
        {
        }

        public AccessDeniedException(string message, Exception innerException) : base(ResultID.E_NETWORK_ERROR, "The server refused the connection.\r\n" + message, innerException)
        {
        }

        protected AccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The server refused the connection.";
    }
}