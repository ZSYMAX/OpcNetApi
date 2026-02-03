using System;
using System.Collections;

namespace Opc.Da
{
    [Serializable]
    public class ItemCollection : ICloneable, IList, ICollection, IEnumerable
    {
        public Item this[int index]
        {
            get { return (Item)this.m_items[index]; }
            set { this.m_items[index] = value; }
        }

        public Item this[ItemIdentifier itemID]
        {
            get
            {
                foreach (object obj in this.m_items)
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
                    this.Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            ItemCollection itemCollection = (ItemCollection)base.MemberwiseClone();
            itemCollection.m_items = new ArrayList();
            foreach (object obj in this.m_items)
            {
                Item item = (Item)obj;
                itemCollection.m_items.Add(item.Clone());
            }

            return itemCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_items == null)
                {
                    return 0;
                }

                return this.m_items.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_items != null)
            {
                this.m_items.CopyTo(array, index);
            }
        }

        public void CopyTo(Item[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_items.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_items[index]; }
            set
            {
                if (!typeof(Item).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add Item objects into the collection.");
                }

                this.m_items[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            this.m_items.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(Item).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Item objects into the collection.");
            }

            this.m_items.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.m_items.Remove(value);
        }

        public bool Contains(object value)
        {
            return this.m_items.Contains(value);
        }

        public void Clear()
        {
            this.m_items.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_items.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(Item).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Item objects into the collection.");
            }

            return this.m_items.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, Item value)
        {
            this.Insert(index, value);
        }

        public void Remove(Item value)
        {
            this.Remove(value);
        }

        public bool Contains(Item value)
        {
            return this.Contains(value);
        }

        public int IndexOf(Item value)
        {
            return this.IndexOf(value);
        }

        public int Add(Item value)
        {
            return this.Add(value);
        }

        private ArrayList m_items = new ArrayList();
    }
}