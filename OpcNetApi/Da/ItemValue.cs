using System;

namespace Opc.Da
{
    [Serializable]
    public class ItemValue : ItemIdentifier
    {
        public object Value
        {
            get { return this.m_value; }
            set { this.m_value = value; }
        }

        public Quality Quality
        {
            get { return this.m_quality; }
            set { this.m_quality = value; }
        }

        public bool QualitySpecified
        {
            get { return this.m_qualitySpecified; }
            set { this.m_qualitySpecified = value; }
        }

        public DateTime Timestamp
        {
            get { return this.m_timestamp; }
            set { this.m_timestamp = value; }
        }

        public bool TimestampSpecified
        {
            get { return this.m_timestampSpecified; }
            set { this.m_timestampSpecified = value; }
        }

        public ItemValue()
        {
        }

        public ItemValue(ItemIdentifier item)
        {
            if (item != null)
            {
                base.ItemName = item.ItemName;
                base.ItemPath = item.ItemPath;
                base.ClientHandle = item.ClientHandle;
                base.ServerHandle = item.ServerHandle;
            }
        }

        public ItemValue(string itemName) : base(itemName)
        {
        }

        public ItemValue(ItemValue item) : base(item)
        {
            if (item != null)
            {
                this.Value = Convert.Clone(item.Value);
                this.Quality = item.Quality;
                this.QualitySpecified = item.QualitySpecified;
                this.Timestamp = item.Timestamp;
                this.TimestampSpecified = item.TimestampSpecified;
            }
        }

        public override object Clone()
        {
            ItemValue itemValue = (ItemValue)base.MemberwiseClone();
            itemValue.Value = Convert.Clone(this.Value);
            return itemValue;
        }

        private object m_value;

        private Quality m_quality = Quality.Bad;

        private bool m_qualitySpecified;

        private DateTime m_timestamp = DateTime.MinValue;

        private bool m_timestampSpecified;
    }
}