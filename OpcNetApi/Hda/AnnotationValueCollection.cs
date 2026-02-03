using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class AnnotationValueCollection : Item, IResult, IActualTime, ICloneable, IList, ICollection, IEnumerable
    {
        public AnnotationValue this[int index]
        {
            get { return (AnnotationValue)this.m_values[index]; }
            set { this.m_values[index] = value; }
        }

        public AnnotationValueCollection()
        {
        }

        public AnnotationValueCollection(ItemIdentifier item) : base(item)
        {
        }

        public AnnotationValueCollection(Item item) : base(item)
        {
        }

        public AnnotationValueCollection(AnnotationValueCollection item) : base(item)
        {
            this.m_values = new ArrayList(item.m_values.Count);
            foreach (object obj in item.m_values)
            {
                ItemValue itemValue = (ItemValue)obj;
                this.m_values.Add(itemValue.Clone());
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
            AnnotationValueCollection annotationValueCollection = (AnnotationValueCollection)base.Clone();
            annotationValueCollection.m_values = new ArrayList(this.m_values.Count);
            foreach (object obj in this.m_values)
            {
                AnnotationValue annotationValue = (AnnotationValue)obj;
                annotationValueCollection.m_values.Add(annotationValue.Clone());
            }

            return annotationValueCollection;
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

        public void CopyTo(AnnotationValue[] array, int index)
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
                if (!typeof(AnnotationValue).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add AnnotationValue objects into the collection.");
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
            if (!typeof(AnnotationValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AnnotationValue objects into the collection.");
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
            if (!typeof(AnnotationValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AnnotationValue objects into the collection.");
            }

            return this.m_values.Add(value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, AnnotationValue value)
        {
            this.Insert(index, value);
        }

        public void Remove(AnnotationValue value)
        {
            this.Remove(value);
        }

        public bool Contains(AnnotationValue value)
        {
            return this.Contains(value);
        }

        public int IndexOf(AnnotationValue value)
        {
            return this.IndexOf(value);
        }

        public int Add(AnnotationValue value)
        {
            return this.Add(value);
        }

        private ArrayList m_values = new ArrayList();

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_endTime = DateTime.MinValue;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}