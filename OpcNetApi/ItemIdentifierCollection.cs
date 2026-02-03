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
            this.Init(collection);
        }

        public ItemIdentifier this[int index]
        {
            get { return this.m_itemIDs[index]; }
            set { this.m_itemIDs[index] = value; }
        }

        public void Init(ICollection collection)
        {
            this.Clear();
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

                this.m_itemIDs = (ItemIdentifier[])arrayList.ToArray(typeof(ItemIdentifier));
            }
        }

        public void Clear()
        {
            this.m_itemIDs = new ItemIdentifier[0];
        }

        public virtual object Clone()
        {
            return new ItemIdentifierCollection(this);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_itemIDs == null)
                {
                    return 0;
                }

                return this.m_itemIDs.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_itemIDs != null)
            {
                this.m_itemIDs.CopyTo(array, index);
            }
        }

        public void CopyTo(ItemIdentifier[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_itemIDs.GetEnumerator();
        }

        private ItemIdentifier[] m_itemIDs = new ItemIdentifier[0];
    }
}