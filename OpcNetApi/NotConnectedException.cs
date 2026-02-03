using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class NotConnectedException : ApplicationException
    {
        public NotConnectedException() : base("The remote server is not currently connected.")
        {
        }

        public NotConnectedException(string message) : base("The remote server is not currently connected.\r\n" + message)
        {
        }

        public NotConnectedException(Exception e) : base("The remote server is not currently connected.", e)
        {
        }

        public NotConnectedException(string message, Exception innerException) : base("The remote server is not currently connected.\r\n" + message, innerException)
        {
        }

        protected NotConnectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The remote server is not currently connected.";
    }
}