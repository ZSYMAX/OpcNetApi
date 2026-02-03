using System;

namespace Opc.Ae
{
    public class EnabledStateResult : IResult
    {
        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        public bool EffectivelyEnabled
        {
            get => m_effectivelyEnabled;
            set => m_effectivelyEnabled = value;
        }

        public ResultID ResultID
        {
            get => m_resultID;
            set => m_resultID = value;
        }

        public string DiagnosticInfo
        {
            get => m_diagnosticInfo;
            set => m_diagnosticInfo = value;
        }

        public EnabledStateResult()
        {
        }

        public EnabledStateResult(string qualifiedName)
        {
            m_qualifiedName = qualifiedName;
        }

        public EnabledStateResult(string qualifiedName, ResultID resultID)
        {
            m_qualifiedName = qualifiedName;
            m_resultID = ResultID;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private string m_qualifiedName;

        private bool m_enabled;

        private bool m_effectivelyEnabled;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}