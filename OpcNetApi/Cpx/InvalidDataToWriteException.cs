using System;
using System.Runtime.Serialization;

namespace Opc.Cpx
{
    [Serializable]
    public class InvalidDataToWriteException : ApplicationException
    {
        public InvalidDataToWriteException() : base("The object cannot be written because it is not consistent with the schema.")
        {
        }

        public InvalidDataToWriteException(string message) : base("The object cannot be written because it is not consistent with the schema.\r\n" + message)
        {
        }

        public InvalidDataToWriteException(Exception e) : base("The object cannot be written because it is not consistent with the schema.", e)
        {
        }

        public InvalidDataToWriteException(string message, Exception innerException) : base("The object cannot be written because it is not consistent with the schema.\r\n" + message, innerException)
        {
        }

        protected InvalidDataToWriteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The object cannot be written because it is not consistent with the schema.";
    }
}