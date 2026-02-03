using System;

namespace Opc.Ae
{
    [Serializable]
    public class AttributeValue : ICloneable, IResult
    {
        public int ID
        {
            get => m_id;
            set => m_id = value;
        }

        public object Value
        {
            get => m_value;
            set => m_value = value;
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

        public virtual object Clone()
        {
            AttributeValue attributeValue = (AttributeValue)MemberwiseClone();
            attributeValue.Value = Convert.Clone(Value);
            return attributeValue;
        }

        private int m_id;

        private object m_value;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}