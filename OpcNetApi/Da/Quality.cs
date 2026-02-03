using System;

namespace Opc.Da
{
    [Serializable]
    public struct Quality
    {
        public qualityBits QualityBits
        {
            get => m_qualityBits;
            set => m_qualityBits = value;
        }

        public limitBits LimitBits
        {
            get => m_limitBits;
            set => m_limitBits = value;
        }

        public byte VendorBits
        {
            get => m_vendorBits;
            set => m_vendorBits = value;
        }

        public short GetCode()
        {
            ushort num = 0;
            num |= (ushort)QualityBits;
            num |= (ushort)LimitBits;
            num |= (ushort)(VendorBits << 8);
            if (num > 32767)
            {
                return (short)(-(short)(65536 - (int)num));
            }

            return (short)num;
        }

        public void SetCode(short code)
        {
            m_qualityBits = (qualityBits)(code & 252);
            m_limitBits = (limitBits)(code & 3);
            m_vendorBits = (byte)((code & -253) >> 8);
        }

        public static bool operator ==(Quality a, Quality b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Quality a, Quality b)
        {
            return !a.Equals(b);
        }

        public Quality(qualityBits quality)
        {
            m_qualityBits = quality;
            m_limitBits = limitBits.none;
            m_vendorBits = 0;
        }

        public Quality(short code)
        {
            m_qualityBits = (qualityBits)(code & 252);
            m_limitBits = (limitBits)(code & 3);
            m_vendorBits = (byte)((code & -253) >> 8);
        }

        public override string ToString()
        {
            string text = QualityBits.ToString();
            if (LimitBits != limitBits.none)
            {
                text += string.Format("[{0}]", LimitBits.ToString());
            }

            if (VendorBits != 0)
            {
                text += string.Format(":{0,0:X}", VendorBits);
            }

            return text;
        }

        public override bool Equals(object target)
        {
            if (target == null || target.GetType() != typeof(Quality))
            {
                return false;
            }

            Quality quality = (Quality)target;
            return QualityBits == quality.QualityBits && LimitBits == quality.LimitBits && VendorBits == quality.VendorBits;
        }

        public override int GetHashCode()
        {
            return (int)GetCode();
        }

        private qualityBits m_qualityBits;

        private limitBits m_limitBits;

        private byte m_vendorBits;

        public static readonly Quality Good = new Quality(qualityBits.good);

        public static readonly Quality Bad = new Quality(qualityBits.bad);
    }
}