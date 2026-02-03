using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class AttributeValueCollection : IResult, ICloneable, IList, ICollection, IEnumerable
    {
        public int AttributeID
        {
            get { return this.m_attributeID; }
            set { this.m_attributeID = value; }
        }

        public AttributeValue this[int index]
        {
            get { return (AttributeValue)this.m_values[index]; }
            set { this.m_values[index] = value; }
        }

        public AttributeValueCollection()
        {
        }

        public AttributeValueCollection(Attribute attribute)
        {
            this.m_attributeID = attribute.ID;
        }

        public AttributeValueCollection(AttributeValueCollection collection)
        {
            this.m_values = new ArrayList(collection.m_values.Count);
            foreach (object obj in collection.m_values)
            {
                AttributeValue attributeValue = (AttributeValue)obj;
                this.m_values.Add(attributeValue.Clone());
            }
        }

        public ResultID ResultID
        {
            get { return this.m_resultID; }
            set { this.m_resultID = value; }
        }

        public string DiagnosticInfo
        {
            get { return this.m_diagnosticInfo; }
            set { this.m_diagnosticInfo = value; }
        }

        public virtual object Clone()
        {
            AttributeValueCollection attributeValueCollection = (AttributeValueCollection)base.MemberwiseClone();
            attributeValueCollection.m_values = new ArrayList(this.m_values.Count);
            foreach (object obj in this.m_values)
            {
                AttributeValue attributeValue = (AttributeValue)obj;
                attributeValueCollection.m_values.Add(attributeValue.Clone());
            }

            return attributeValueCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_values == null)
                {
                    return 0;
                }

                return this.m_values.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_values != null)
            {
                this.m_values.CopyTo(array, index);
            }
        }

        public void CopyTo(AttributeValue[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_values.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_values[index]; }
            set
            {
                if (!typeof(AttributeValue).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add AttributeValue objects into the collection.");
                }

                this.m_values[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            this.m_values.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(AttributeValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValue objects into the collection.");
            }

            this.m_values.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.m_values.Remove(value);
        }

        public bool Contains(object value)
        {
            return this.m_values.Contains(value);
        }

        public void Clear()
        {
            this.m_values.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_values.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(AttributeValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValue objects into the collection.");
            }

            return this.m_values.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, AttributeValue value)
        {
            this.Insert(index, value);
        }

        public void Remove(AttributeValue value)
        {
            this.Remove(value);
        }

        public bool Contains(AttributeValue value)
        {
            return this.Contains(value);
        }

        public int IndexOf(AttributeValue value)
        {
            return this.IndexOf(value);
        }

        public int Add(AttributeValue value)
        {
            return this.Add(value);
        }

        private int m_attributeID;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;

        private ArrayList m_values = new ArrayList();
    }
}