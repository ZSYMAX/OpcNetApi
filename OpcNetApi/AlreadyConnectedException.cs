using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class AlreadyConnectedException : ApplicationException
    {
        public AlreadyConnectedException() : base("The remote server is already connected.")
        {
        }

        public AlreadyConnectedException(string message) : base("The remote server is already connected.\r\n" + message)
        {
        }

        public AlreadyConnectedException(Exception e) : base("The remote server is already connected.", e)
        {
        }

        public AlreadyConnectedException(string message, Exception innerException) : base("The remote server is already connected.\r\n" + message, innerException)
        {
        }

        protected AlreadyConnectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The remote server is already connected.";
    }
}