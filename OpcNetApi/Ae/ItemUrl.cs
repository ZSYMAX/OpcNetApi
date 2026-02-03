using System;

namespace Opc.Ae
{
    [Serializable]
    public class ItemUrl : ItemIdentifier
    {
        public URL Url
        {
            get { return this.m_url; }
            set { this.m_url = value; }
        }

        public ItemUrl()
        {
        }

        public ItemUrl(ItemIdentifier item) : base(item)
        {
        }

        public ItemUrl(ItemIdentifier item, URL url) : base(item)
        {
            this.Url = url;
        }

        public ItemUrl(ItemUrl item) : base(item)
        {
            if (item != null)
            {
                this.Url = item.Url;
            }
        }

        public override object Clone()
        {
            return new ItemUrl(this);
        }

        private URL m_url = new URL();
    }
}