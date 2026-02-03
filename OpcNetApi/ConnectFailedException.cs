using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class ConnectFailedException : ResultIDException
    {
        public ConnectFailedException() : base(ResultID.E_ACCESS_DENIED, "Could not connect to server.")
        {
        }

        public ConnectFailedException(string message) : base(ResultID.E_NETWORK_ERROR, "Could not connect to server.\r\n" + message)
        {
        }

        public ConnectFailedException(Exception e) : base(ResultID.E_NETWORK_ERROR, "Could not connect to server.", e)
        {
        }

        public ConnectFailedException(string message, Exception innerException) : base(ResultID.E_NETWORK_ERROR, "Could not connect to server.\r\n" + message, innerException)
        {
        }

        protected ConnectFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "Could not connect to server.";
    }
}