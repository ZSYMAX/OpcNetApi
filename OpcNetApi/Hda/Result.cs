using System;

namespace Opc.Hda
{
    [Serializable]
    public class Result : ICloneable, IResult
    {
        public Result()
        {
        }

        public Result(ResultID resultID)
        {
            this.ResultID = resultID;
            this.DiagnosticInfo = null;
        }

        public Result(IResult result)
        {
            this.ResultID = result.ResultID;
            this.DiagnosticInfo = result.DiagnosticInfo;
        }

        public ResultID ResultID
        {
            get { return this.m_resultID; }
            set { this.m_resultID = value; }
        }

        public string DiagnosticInfo
        {
            get { return this.m_diagnosticInfo; }
            set { this.m_diagnosticInfo = value; }
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}