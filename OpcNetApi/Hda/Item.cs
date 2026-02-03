using System;

namespace Opc.Hda
{
    [Serializable]
    public class Item : ItemIdentifier
    {
        public int AggregateID
        {
            get => m_aggregateID;
            set => m_aggregateID = value;
        }

        public Item()
        {
        }

        public Item(ItemIdentifier item) : base(item)
        {
        }

        public Item(Item item) : base(item)
        {
            if (item != null)
            {
                AggregateID = item.AggregateID;
            }
        }

        private int m_aggregateID;
    }
}