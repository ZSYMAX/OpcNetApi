using System;

namespace Opc.Ae
{
    public class EnabledStateResult : IResult
    {
        public bool Enabled
        {
            get { return this.m_enabled; }
            set { this.m_enabled = value; }
        }

        public bool EffectivelyEnabled
        {
            get { return this.m_effectivelyEnabled; }
            set { this.m_effectivelyEnabled = value; }
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

        public EnabledStateResult()
        {
        }

        public EnabledStateResult(string qualifiedName)
        {
            this.m_qualifiedName = qualifiedName;
        }

        public EnabledStateResult(string qualifiedName, ResultID resultID)
        {
            this.m_qualifiedName = qualifiedName;
            this.m_resultID = this.ResultID;
        }

        public virtual object Clone()
        {
            return base.MemberwiseClone();
        }

        private string m_qualifiedName;

        private bool m_enabled;

        private bool m_effectivelyEnabled;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}