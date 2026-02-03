using System;
using System.Collections;

namespace Opc
{
    [Serializable]
    public class ItemIdentifierCollection : ICloneable, ICollection, IEnumerable
    {
        public ItemIdentifierCollection()
        {
        }

        public ItemIdentifierCollection(ICollection collection)
        {
            Init(collection);
        }

        public ItemIdentifier this[int index]
        {
            get => m_itemIDs[index];
            set => m_itemIDs[index] = value;
        }

        public void Init(ICollection collection)
        {
            Clear();
            if (collection != null)
            {
                ArrayList arrayList = new ArrayList(collection.Count);
                foreach (object obj in collection)
                {
                    if (typeof(ItemIdentifier).IsInstanceOfType(obj))
                    {
                        arrayList.Add(((ItemIdentifier)obj).Clone());
                    }
                }

                m_itemIDs = (ItemIdentifier[])arrayList.ToArray(typeof(ItemIdentifier));
            }
        }

        public void Clear()
        {
            m_itemIDs = new ItemIdentifier[0];
        }

        public virtual object Clone()
        {
            return new ItemIdentifierCollection(this);
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_itemIDs == null)
                {
                    return 0;
                }

                return m_itemIDs.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_itemIDs != null)
            {
                m_itemIDs.CopyTo(array, index);
            }
        }

        public void CopyTo(ItemIdentifier[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_itemIDs.GetEnumerator();
        }

        private ItemIdentifier[] m_itemIDs = new ItemIdentifier[0];
    }
}