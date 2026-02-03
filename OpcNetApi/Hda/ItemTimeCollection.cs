using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ItemTimeCollection : ItemIdentifier, ICloneable, IList, ICollection, IEnumerable
    {
        public DateTime this[int index]
        {
            get => (DateTime)m_times[index];
            set => m_times[index] = value;
        }

        public ItemTimeCollection()
        {
        }

        public ItemTimeCollection(ItemIdentifier item) : base(item)
        {
        }

        public ItemTimeCollection(ItemTimeCollection item) : base(item)
        {
            m_times = new ArrayList(item.m_times.Count);
            foreach (object obj in item.m_times)
            {
                DateTime dateTime = (DateTime)obj;
                m_times.Add(dateTime);
            }
        }

        public override object Clone()
        {
            ItemTimeCollection itemTimeCollection = (ItemTimeCollection)base.Clone();
            itemTimeCollection.m_times = new ArrayList(m_times.Count);
            foreach (object obj in m_times)
            {
                DateTime dateTime = (DateTime)obj;
                itemTimeCollection.m_times.Add(dateTime);
            }

            return itemTimeCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_times == null)
                {
                    return 0;
                }

                return m_times.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_times != null)
            {
                m_times.CopyTo(array, index);
            }
        }

        public void CopyTo(DateTime[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_times.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_times[index];
            set
            {
                if (!typeof(DateTime).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add DateTime objects into the collection.");
                }

                m_times[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            m_times.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(DateTime).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DateTime objects into the collection.");
            }

            m_times.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_times.Remove(value);
        }

        public bool Contains(object value)
        {
            return m_times.Contains(value);
        }

        public void Clear()
        {
            m_times.Clear();
        }

        public int IndexOf(object value)
        {
            return m_times.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(DateTime).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DateTime objects into the collection.");
            }

            return m_times.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, DateTime value)
        {
            Insert(index, value);
        }

        public void Remove(DateTime value)
        {
            Remove(value);
        }

        public bool Contains(DateTime value)
        {
            return Contains(value);
        }

        public int IndexOf(DateTime value)
        {
            return IndexOf(value);
        }

        public int Add(DateTime value)
        {
            return Add(value);
        }

        private ArrayList m_times = new ArrayList();
    }
}