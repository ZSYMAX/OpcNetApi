using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ItemCollection : ICloneable, IList, ICollection, IEnumerable
    {
        public Item this[int index]
        {
            get => (Item)m_items[index];
            set => m_items[index] = value;
        }

        public Item this[ItemIdentifier itemID]
        {
            get
            {
                foreach (object obj in m_items)
                {
                    Item item = (Item)obj;
                    if (itemID.Key == item.Key)
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        public ItemCollection()
        {
        }

        public ItemCollection(ItemCollection items)
        {
            if (items != null)
            {
                foreach (object obj in items)
                {
                    Item value = (Item)obj;
                    Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            ItemCollection itemCollection = (ItemCollection)MemberwiseClone();
            itemCollection.m_items = new ArrayList();
            foreach (object obj in m_items)
            {
                Item item = (Item)obj;
                itemCollection.m_items.Add(item.Clone());
            }

            return itemCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_items == null)
                {
                    return 0;
                }

                return m_items.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_items != null)
            {
                m_items.CopyTo(array, index);
            }
        }

        public void CopyTo(Item[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_items[index];
            set
            {
                if (!typeof(Item).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add Item objects into the collection.");
                }

                m_items[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            m_items.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(Item).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Item objects into the collection.");
            }

            m_items.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_items.Remove(value);
        }

        public bool Contains(object value)
        {
            return m_items.Contains(value);
        }

        public void Clear()
        {
            m_items.Clear();
        }

        public int IndexOf(object value)
        {
            return m_items.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(Item).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Item objects into the collection.");
            }

            return m_items.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, Item value)
        {
            Insert(index, value);
        }

        public void Remove(Item value)
        {
            Remove(value);
        }

        public bool Contains(Item value)
        {
            return Contains(value);
        }

        public int IndexOf(Item value)
        {
            return IndexOf(value);
        }

        public int Add(Item value)
        {
            return Add(value);
        }

        private ArrayList m_items = new ArrayList();
    }
}