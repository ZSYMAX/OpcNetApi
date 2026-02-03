using System;
using System.Runtime.Serialization;

namespace Opc
{
    [Serializable]
    public class ResultIDException : ApplicationException
    {
        public ResultID Result
        {
            get { return this.m_result; }
        }

        public ResultIDException(ResultID result) : base(result.ToString())
        {
            this.m_result = result;
        }

        public ResultIDException(ResultID result, string message) : base(result.ToString() + "\r\n" + message)
        {
            this.m_result = result;
        }

        public ResultIDException(ResultID result, string message, Exception e) : base(result.ToString() + "\r\n" + message, e)
        {
            this.m_result = result;
        }

        protected ResultIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private ResultID m_result = ResultID.E_FAIL;
    }
}