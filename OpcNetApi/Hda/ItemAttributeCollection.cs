using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ItemAttributeCollection : ItemIdentifier, IResult, IActualTime, IList, ICollection, IEnumerable
    {
        public AttributeValueCollection this[int index]
        {
            get => (AttributeValueCollection)m_attributes[index];
            set => m_attributes[index] = value;
        }

        public ItemAttributeCollection()
        {
        }

        public ItemAttributeCollection(ItemIdentifier item) : base(item)
        {
        }

        public ItemAttributeCollection(ItemAttributeCollection item) : base(item)
        {
            m_attributes = new ArrayList(item.m_attributes.Count);
            foreach (object obj in item.m_attributes)
            {
                AttributeValueCollection attributeValueCollection = (AttributeValueCollection)obj;
                if (attributeValueCollection != null)
                {
                    m_attributes.Add(attributeValueCollection.Clone());
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
            ItemAttributeCollection itemAttributeCollection = (ItemAttributeCollection)base.Clone();
            itemAttributeCollection.m_attributes = new ArrayList(m_attributes.Count);
            foreach (object obj in m_attributes)
            {
                AttributeValueCollection attributeValueCollection = (AttributeValueCollection)obj;
                itemAttributeCollection.m_attributes.Add(attributeValueCollection.Clone());
            }

            return itemAttributeCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_attributes == null)
                {
                    return 0;
                }

                return m_attributes.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_attributes != null)
            {
                m_attributes.CopyTo(array, index);
            }
        }

        public void CopyTo(AttributeValueCollection[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_attributes.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_attributes[index];
            set
            {
                if (!typeof(AttributeValueCollection).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add AttributeValueCollection objects into the collection.");
                }

                m_attributes[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            m_attributes.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(AttributeValueCollection).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValueCollection objects into the collection.");
            }

            m_attributes.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_attributes.Remove(value);
        }

        public bool Contains(object value)
        {
            return m_attributes.Contains(value);
        }

        public void Clear()
        {
            m_attributes.Clear();
        }

        public int IndexOf(object value)
        {
            return m_attributes.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(AttributeValueCollection).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValueCollection objects into the collection.");
            }

            return m_attributes.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, AttributeValueCollection value)
        {
            Insert(index, value);
        }

        public void Remove(AttributeValueCollection value)
        {
            Remove(value);
        }

        public bool Contains(AttributeValueCollection value)
        {
            return Contains(value);
        }

        public int IndexOf(AttributeValueCollection value)
        {
            return IndexOf(value);
        }

        public int Add(AttributeValueCollection value)
        {
            return Add(value);
        }

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_endTime = DateTime.MinValue;

        private ArrayList m_attributes = new ArrayList();

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}