using System;

namespace Opc.Da
{
    [Serializable]
    public class ItemValue : ItemIdentifier
    {
        public object Value
        {
            get => m_value;
            set => m_value = value;
        }

        public Quality Quality
        {
            get => m_quality;
            set => m_quality = value;
        }

        public bool QualitySpecified
        {
            get => m_qualitySpecified;
            set => m_qualitySpecified = value;
        }

        public DateTime Timestamp
        {
            get => m_timestamp;
            set => m_timestamp = value;
        }

        public bool TimestampSpecified
        {
            get => m_timestampSpecified;
            set => m_timestampSpecified = value;
        }

        public ItemValue()
        {
        }

        public ItemValue(ItemIdentifier item)
        {
            if (item != null)
            {
                ItemName = item.ItemName;
                ItemPath = item.ItemPath;
                ClientHandle = item.ClientHandle;
                ServerHandle = item.ServerHandle;
            }
        }

        public ItemValue(string itemName) : base(itemName)
        {
        }

        public ItemValue(ItemValue item) : base(item)
        {
            if (item != null)
            {
                Value = Convert.Clone(item.Value);
                Quality = item.Quality;
                QualitySpecified = item.QualitySpecified;
                Timestamp = item.Timestamp;
                TimestampSpecified = item.TimestampSpecified;
            }
        }

        public override object Clone()
        {
            ItemValue itemValue = (ItemValue)MemberwiseClone();
            itemValue.Value = Convert.Clone(Value);
            return itemValue;
        }

        private object m_value;

        private Quality m_quality = Quality.Bad;

        private bool m_qualitySpecified;

        private DateTime m_timestamp = DateTime.MinValue;

        private bool m_timestampSpecified;
    }
}