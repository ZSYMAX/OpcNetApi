using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ItemAttributeCollection : ItemIdentifier, IResult, IActualTime, IList, ICollection, IEnumerable
    {
        public AttributeValueCollection this[int index]
        {
            get { return (AttributeValueCollection)this.m_attributes[index]; }
            set { this.m_attributes[index] = value; }
        }

        public ItemAttributeCollection()
        {
        }

        public ItemAttributeCollection(ItemIdentifier item) : base(item)
        {
        }

        public ItemAttributeCollection(ItemAttributeCollection item) : base(item)
        {
            this.m_attributes = new ArrayList(item.m_attributes.Count);
            foreach (object obj in item.m_attributes)
            {
                AttributeValueCollection attributeValueCollection = (AttributeValueCollection)obj;
                if (attributeValueCollection != null)
                {
                    this.m_attributes.Add(attributeValueCollection.Clone());
                }
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

        public DateTime StartTime
        {
            get { return this.m_startTime; }
            set { this.m_startTime = value; }
        }

        public DateTime EndTime
        {
            get { return this.m_endTime; }
            set { this.m_endTime = value; }
        }

        public override object Clone()
        {
            ItemAttributeCollection itemAttributeCollection = (ItemAttributeCollection)base.Clone();
            itemAttributeCollection.m_attributes = new ArrayList(this.m_attributes.Count);
            foreach (object obj in this.m_attributes)
            {
                AttributeValueCollection attributeValueCollection = (AttributeValueCollection)obj;
                itemAttributeCollection.m_attributes.Add(attributeValueCollection.Clone());
            }

            return itemAttributeCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_attributes == null)
                {
                    return 0;
                }

                return this.m_attributes.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_attributes != null)
            {
                this.m_attributes.CopyTo(array, index);
            }
        }

        public void CopyTo(AttributeValueCollection[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_attributes.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_attributes[index]; }
            set
            {
                if (!typeof(AttributeValueCollection).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add AttributeValueCollection objects into the collection.");
                }

                this.m_attributes[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            this.m_attributes.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(AttributeValueCollection).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValueCollection objects into the collection.");
            }

            this.m_attributes.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.m_attributes.Remove(value);
        }

        public bool Contains(object value)
        {
            return this.m_attributes.Contains(value);
        }

        public void Clear()
        {
            this.m_attributes.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_attributes.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(AttributeValueCollection).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AttributeValueCollection objects into the collection.");
            }

            return this.m_attributes.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, AttributeValueCollection value)
        {
            this.Insert(index, value);
        }

        public void Remove(AttributeValueCollection value)
        {
            this.Remove(value);
        }

        public bool Contains(AttributeValueCollection value)
        {
            return this.Contains(value);
        }

        public int IndexOf(AttributeValueCollection value)
        {
            return this.IndexOf(value);
        }

        public int Add(AttributeValueCollection value)
        {
            return this.Add(value);
        }

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_endTime = DateTime.MinValue;

        private ArrayList m_attributes = new ArrayList();

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}