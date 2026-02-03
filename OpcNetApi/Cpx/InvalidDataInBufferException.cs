using System;
using System.Runtime.Serialization;

namespace Opc.Cpx
{
    [Serializable]
    public class InvalidDataInBufferException : ApplicationException
    {
        public InvalidDataInBufferException() : base("The data in the buffer cannot be read because it is not consistent with the schema.")
        {
        }

        public InvalidDataInBufferException(string message) : base("The data in the buffer cannot be read because it is not consistent with the schema.\r\n" + message)
        {
        }

        public InvalidDataInBufferException(Exception e) : base("The data in the buffer cannot be read because it is not consistent with the schema.", e)
        {
        }

        public InvalidDataInBufferException(string message, Exception innerException) : base("The data in the buffer cannot be read because it is not consistent with the schema.\r\n" + message, innerException)
        {
        }

        protected InvalidDataInBufferException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The data in the buffer cannot be read because it is not consistent with the schema.";
    }
}