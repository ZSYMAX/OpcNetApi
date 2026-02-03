using System;

namespace Opc.Hda
{
    [Serializable]
    public class ItemValue : ICloneable
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

        public Opc.Da.Quality Quality
        {
            get { return this.m_quality; }
            set { this.m_quality = value; }
        }

        public Quality HistorianQuality
        {
            get { return this.m_historianQuality; }
            set { this.m_historianQuality = value; }
        }

        public object Clone()
        {
            ItemValue itemValue = (ItemValue)base.MemberwiseClone();
            itemValue.Value = Convert.Clone(this.Value);
            return itemValue;
        }

        private object m_value;

        private DateTime m_timestamp = DateTime.MinValue;

        private Opc.Da.Quality m_quality = Opc.Da.Quality.Bad;

        private Quality m_historianQuality = Opc.Hda.Quality.NoData;
    }
}