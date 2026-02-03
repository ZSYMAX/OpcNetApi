using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class AttributeValueCollection : IResult, ICloneable, IList, ICollection, IEnumerable
    {
        public int AttributeID
        {
            get => m_attributeID;
            set => m_attributeID = value;
        }

        public AttributeValue this[int index]
        {
            get => (AttributeValue)m_values[index];
            set => m_values[index] = value;
        }

        public AttributeValueCollection()
        {
        }

        public AttributeValueCollection(Attribute attribute)
        {
            m_attributeID = attribute.ID;
        }

        public AttributeValueCollection(AttributeValueCollection collection)
        {
            m_values = new ArrayList(collection.m_values.Count);
            foreach (object obj in collection.m_values)
            {
                AttributeValue attributeValue = (AttributeValue)obj;
                m_values.Add(attributeValue.Clone());
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

        public virtual object Clone()
        {
            AttributeValueCollection attributeValueCollection = (AttributeValueCollection)MemberwiseClone();
            attributeValueCollection.m_values = new ArrayList(m_values.Count);
            foreach (object obj in m_values)
            {
                AttributeValue attributeValue = (AttributeValue)obj;
                attributeValueCollection.m_values.Add(attributeValue.Clone());
            }

            return attributeValueCollection;
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

        public void CopyTo(AttributeValue[] array, int index)
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
                if (!typeof(AttributeValue).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add AttributeValue objects into the collection.");
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
            if (!typeof(AttributeValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValue objects into the collection.");
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
            if (!typeof(AttributeValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValue objects into the collection.");
            }

            return m_values.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, AttributeValue value)
        {
            Insert(index, value);
        }

        public void Remove(AttributeValue value)
        {
            Remove(value);
        }

        public bool Contains(AttributeValue value)
        {
            return Contains(value);
        }

        public int IndexOf(AttributeValue value)
        {
            return IndexOf(value);
        }

        public int Add(AttributeValue value)
        {
            return Add(value);
        }

        private int m_attributeID;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;

        private ArrayList m_values = new ArrayList();
    }
}