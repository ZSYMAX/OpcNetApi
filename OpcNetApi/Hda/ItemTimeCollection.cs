using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ItemTimeCollection : ItemIdentifier, ICloneable, IList, ICollection, IEnumerable
    {
        public DateTime this[int index]
        {
            get { return (DateTime)this.m_times[index]; }
            set { this.m_times[index] = value; }
        }

        public ItemTimeCollection()
        {
        }

        public ItemTimeCollection(ItemIdentifier item) : base(item)
        {
        }

        public ItemTimeCollection(ItemTimeCollection item) : base(item)
        {
            this.m_times = new ArrayList(item.m_times.Count);
            foreach (object obj in item.m_times)
            {
                DateTime dateTime = (DateTime)obj;
                this.m_times.Add(dateTime);
            }
        }

        public override object Clone()
        {
            ItemTimeCollection itemTimeCollection = (ItemTimeCollection)base.Clone();
            itemTimeCollection.m_times = new ArrayList(this.m_times.Count);
            foreach (object obj in this.m_times)
            {
                DateTime dateTime = (DateTime)obj;
                itemTimeCollection.m_times.Add(dateTime);
            }

            return itemTimeCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_times == null)
                {
                    return 0;
                }

                return this.m_times.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_times != null)
            {
                this.m_times.CopyTo(array, index);
            }
        }

        public void CopyTo(DateTime[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_times.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_times[index]; }
            set
            {
                if (!typeof(DateTime).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add DateTime objects into the collection.");
                }

                this.m_times[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            this.m_times.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(DateTime).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DateTime objects into the collection.");
            }

            this.m_times.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.m_times.Remove(value);
        }

        public bool Contains(object value)
        {
            return this.m_times.Contains(value);
        }

        public void Clear()
        {
            this.m_times.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_times.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(DateTime).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DateTime objects into the collection.");
            }

            return this.m_times.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, DateTime value)
        {
            this.Insert(index, value);
        }

        public void Remove(DateTime value)
        {
            this.Remove(value);
        }

        public bool Contains(DateTime value)
        {
            return this.Contains(value);
        }

        public int IndexOf(DateTime value)
        {
            return this.IndexOf(value);
        }

        public int Add(DateTime value)
        {
            return this.Add(value);
        }

        private ArrayList m_times = new ArrayList();
    }
}