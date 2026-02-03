using System;

namespace Opc.Ae
{
    [Serializable]
    public class ItemUrl : ItemIdentifier
    {
        public URL Url
        {
            get => m_url;
            set => m_url = value;
        }

        public ItemUrl()
        {
        }

        public ItemUrl(ItemIdentifier item) : base(item)
        {
        }

        public ItemUrl(ItemIdentifier item, URL url) : base(item)
        {
            Url = url;
        }

        public ItemUrl(ItemUrl item) : base(item)
        {
            if (item != null)
            {
                Url = item.Url;
            }
        }

        public override object Clone()
        {
            return new ItemUrl(this);
        }

        private URL m_url = new URL();
    }
}