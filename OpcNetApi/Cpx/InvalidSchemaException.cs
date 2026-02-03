using System;
using System.Runtime.Serialization;

namespace Opc.Cpx
{
    [Serializable]
    public class InvalidSchemaException : ApplicationException
    {
        public InvalidSchemaException() : base("The schema cannot be used because it contains errors or inconsitencies.")
        {
        }

        public InvalidSchemaException(string message) : base("The schema cannot be used because it contains errors or inconsitencies.\r\n" + message)
        {
        }

        public InvalidSchemaException(Exception e) : base("The schema cannot be used because it contains errors or inconsitencies.", e)
        {
        }

        public InvalidSchemaException(string message, Exception innerException) : base("The schema cannot be used because it contains errors or inconsitencies.\r\n" + message, innerException)
        {
        }

        protected InvalidSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The schema cannot be used because it contains errors or inconsitencies.";
    }
}