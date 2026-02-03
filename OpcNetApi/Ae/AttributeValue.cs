using System;

namespace Opc.Ae
{
    [Serializable]
    public class AttributeValue : ICloneable, IResult
    {
        public int ID
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        public object Value
        {
            get { return this.m_value; }
            set { this.m_value = value; }
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

        public virtual object Clone()
        {
            AttributeValue attributeValue = (AttributeValue)base.MemberwiseClone();
            attributeValue.Value = Convert.Clone(this.Value);
            return attributeValue;
        }

        private int m_id;

        private object m_value;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}