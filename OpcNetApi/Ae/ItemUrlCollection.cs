using System;

namespace Opc.Ae
{
    public class ItemUrlCollection : ReadOnlyCollection
    {
        public ItemUrl this[int index]
        {
            get { return (ItemUrl)this.Array.GetValue(index); }
        }

        public new ItemUrl[] ToArray()
        {
            return (ItemUrl[])Convert.Clone(this.Array);
        }

        public ItemUrlCollection() : base(new ItemUrl[0])
        {
        }

        public ItemUrlCollection(ItemUrl[] itemUrls) : base(itemUrls)
        {
        }
    }
}