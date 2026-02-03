using System;

namespace Opc.Da
{
    [Serializable]
    public struct Quality
    {
        public qualityBits QualityBits
        {
            get { return this.m_qualityBits; }
            set { this.m_qualityBits = value; }
        }

        public limitBits LimitBits
        {
            get { return this.m_limitBits; }
            set { this.m_limitBits = value; }
        }

        public byte VendorBits
        {
            get { return this.m_vendorBits; }
            set { this.m_vendorBits = value; }
        }

        public short GetCode()
        {
            ushort num = 0;
            num |= (ushort)this.QualityBits;
            num |= (ushort)this.LimitBits;
            num |= (ushort)(this.VendorBits << 8);
            if (num > 32767)
            {
                return (short)(-(short)(65536 - (int)num));
            }

            return (short)num;
        }

        public void SetCode(short code)
        {
            this.m_qualityBits = (qualityBits)(code & 252);
            this.m_limitBits = (limitBits)(code & 3);
            this.m_vendorBits = (byte)((code & -253) >> 8);
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
            this.m_qualityBits = quality;
            this.m_limitBits = limitBits.none;
            this.m_vendorBits = 0;
        }

        public Quality(short code)
        {
            this.m_qualityBits = (qualityBits)(code & 252);
            this.m_limitBits = (limitBits)(code & 3);
            this.m_vendorBits = (byte)((code & -253) >> 8);
        }

        public override string ToString()
        {
            string text = this.QualityBits.ToString();
            if (this.LimitBits != limitBits.none)
            {
                text += string.Format("[{0}]", this.LimitBits.ToString());
            }

            if (this.VendorBits != 0)
            {
                text += string.Format(":{0,0:X}", this.VendorBits);
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
            return this.QualityBits == quality.QualityBits && this.LimitBits == quality.LimitBits && this.VendorBits == quality.VendorBits;
        }

        public override int GetHashCode()
        {
            return (int)this.GetCode();
        }

        private qualityBits m_qualityBits;

        private limitBits m_limitBits;

        private byte m_vendorBits;

        public static readonly Quality Good = new Quality(qualityBits.good);

        public static readonly Quality Bad = new Quality(qualityBits.bad);
    }
}