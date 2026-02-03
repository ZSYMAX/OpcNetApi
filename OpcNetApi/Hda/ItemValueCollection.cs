using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ItemValueCollection : Item, IResult, IActualTime, ICloneable, IList, ICollection, IEnumerable
    {
        public ItemValue this[int index]
        {
            get => (ItemValue)m_values[index];
            set => m_values[index] = value;
        }

        public ItemValueCollection()
        {
        }

        public ItemValueCollection(ItemIdentifier item) : base(item)
        {
        }

        public ItemValueCollection(Item item) : base(item)
        {
        }

        public ItemValueCollection(ItemValueCollection item) : base(item)
        {
            m_values = new ArrayList(item.m_values.Count);
            foreach (object obj in item.m_values)
            {
                ItemValue itemValue = (ItemValue)obj;
                if (itemValue != null)
                {
                    m_values.Add(itemValue.Clone());
                }
            }
        }

        public void AddRange(ItemValueCollection collection)
        {
            if (collection != null)
            {
                foreach (object obj in collection)
                {
                    ItemValue itemValue = (ItemValue)obj;
                    if (itemValue != null)
                    {
                        m_values.Add(itemValue.Clone());
                    }
                }
            }
        }

        public ResultID ResultID
        {
            get => m_resultID;
            set => m_resultID = value;
        }

        public string DiagnosticInfo
        {
            get => m_diagnosticInfo;
            set => m_diagnosticInfo = value;
        }

        public DateTime StartTime
        {
            get => m_startTime;
            set => m_startTime = value;
        }

        public DateTime EndTime
        {
            get => m_endTime;
            set => m_endTime = value;
        }

        public override object Clone()
        {
            ItemValueCollection itemValueCollection = (ItemValueCollection)base.Clone();
            itemValueCollection.m_values = new ArrayList(m_values.Count);
            foreach (object obj in m_values)
            {
                ItemValue itemValue = (ItemValue)obj;
                itemValueCollection.m_values.Add(itemValue.Clone());
            }

            return itemValueCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_values == null)
                {
                    return 0;
                }

                return m_values.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_values != null)
            {
                m_values.CopyTo(array, index);
            }
        }

        public void CopyTo(ItemValue[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_values[index];
            set
            {
                if (!typeof(ItemValue).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add ItemValue objects into the collection.");
                }

                m_values[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            m_values.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(ItemValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add ItemValue objects into the collection.");
            }

            m_values.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_values.Remove(value);
        }

        public bool Contains(object value)
        {
            return m_values.Contains(value);
        }

        public void Clear()
        {
            m_values.Clear();
        }

        public int IndexOf(object value)
        {
            return m_values.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(ItemValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add ItemValue objects into the collection.");
            }

            return m_values.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, ItemValue value)
        {
            Insert(index, value);
        }

        public void Remove(ItemValue value)
        {
            Remove(value);
        }

        public bool Contains(ItemValue value)
        {
            return Contains(value);
        }

        public int IndexOf(ItemValue value)
        {
            return IndexOf(value);
        }

        public int Add(ItemValue value)
        {
            return Add(value);
        }

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_endTime = DateTime.MinValue;

        private ArrayList m_values = new ArrayList();

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}