using System;

namespace Opc.Hda
{
    [Serializable]
    public struct TimeOffset
    {
        public int Value
        {
            get { return this.m_value; }
            set { this.m_value = value; }
        }

        public RelativeTime Type
        {
            get { return this.m_type; }
            set { this.m_type = value; }
        }

        internal static string OffsetTypeToString(RelativeTime offsetType)
        {
            switch (offsetType)
            {
                case RelativeTime.Second:
                    return "S";
                case RelativeTime.Minute:
                    return "M";
                case RelativeTime.Hour:
                    return "H";
                case RelativeTime.Day:
                    return "D";
                case RelativeTime.Week:
                    return "W";
                case RelativeTime.Month:
                    return "MO";
                case RelativeTime.Year:
                    return "Y";
                default:
                    throw new ArgumentOutOfRangeException("offsetType", offsetType.ToString(), "Invalid value for relative time offset type.");
            }
        }

        private int m_value;

        private RelativeTime m_type;
    }
}