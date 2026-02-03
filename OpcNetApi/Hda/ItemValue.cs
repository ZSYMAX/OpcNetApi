using System;

namespace Opc.Hda
{
    [Serializable]
    public class ItemValue : ICloneable
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

        public Opc.Da.Quality Quality
        {
            get => m_quality;
            set => m_quality = value;
        }

        public Quality HistorianQuality
        {
            get => m_historianQuality;
            set => m_historianQuality = value;
        }

        public object Clone()
        {
            ItemValue itemValue = (ItemValue)MemberwiseClone();
            itemValue.Value = Convert.Clone(Value);
            return itemValue;
        }

        private object m_value;

        private DateTime m_timestamp = DateTime.MinValue;

        private Opc.Da.Quality m_quality = Da.Quality.Bad;

        private Quality m_historianQuality = Opc.Hda.Quality.NoData;
    }
}