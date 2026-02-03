using System;

namespace Opc.Hda
{
    [Serializable]
    public class AttributeValue : ICloneable
    {
        public object Value
        {
            get => m_value;
            set => m_value = value;
        }

        public DateTime Timestamp
        {
            get => m_timestamp;
            set => m_timestamp = value;
        }

        public virtual object Clone()
        {
            AttributeValue attributeValue = (AttributeValue)MemberwiseClone();
            attributeValue.m_value = Convert.Clone(m_value);
            return attributeValue;
        }

        private object m_value;

        private DateTime m_timestamp = DateTime.MinValue;
    }
}