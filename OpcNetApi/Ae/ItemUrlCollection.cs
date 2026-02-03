using System;

namespace Opc.Ae
{
    public class ItemUrlCollection : ReadOnlyCollection
    {
        public ItemUrl this[int index] => (ItemUrl)Array.GetValue(index);

        public new ItemUrl[] ToArray()
        {
            return (ItemUrl[])Convert.Clone(Array);
        }

        public ItemUrlCollection() : base(new ItemUrl[0])
        {
        }

        public ItemUrlCollection(ItemUrl[] itemUrls) : base(itemUrls)
        {
        }
    }
}