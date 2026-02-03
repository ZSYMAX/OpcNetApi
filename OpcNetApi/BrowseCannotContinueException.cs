using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class BrowseCannotContinueException : ApplicationException
    {
        public BrowseCannotContinueException() : base("The browse operation cannot continue.")
        {
        }

        public BrowseCannotContinueException(string message) : base("The browse operation cannot continue.\r\n" + message)
        {
        }

        public BrowseCannotContinueException(string message, Exception innerException) : base("The browse operation cannot continue.\r\n" + message, innerException)
        {
        }

        protected BrowseCannotContinueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private const string Default = "The browse operation cannot continue.";
    }
}