using System;

namespace Opc.Hda
{
    [Serializable]
    public class AttributeValue : ICloneable
    {
        public object Value
        {
            get { return this.m_value; }
            set { this.m_value = value; }
        }

        public DateTime Timestamp
        {
            get { return this.m_timestamp; }
            set { this.m_timestamp = value; }
        }

        public virtual object Clone()
        {
            AttributeValue attributeValue = (AttributeValue)base.MemberwiseClone();
            attributeValue.m_value = Convert.Clone(this.m_value);
            return attributeValue;
        }

        private object m_value;

        private DateTime m_timestamp = DateTime.MinValue;
    }
}