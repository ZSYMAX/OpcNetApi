using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class AnnotationValueCollection : Item, IResult, IActualTime, ICloneable, IList, ICollection, IEnumerable
    {
        public AnnotationValue this[int index]
        {
            get => (AnnotationValue)m_values[index];
            set => m_values[index] = value;
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
            m_values = new ArrayList(item.m_values.Count);
            foreach (object obj in item.m_values)
            {
                ItemValue itemValue = (ItemValue)obj;
                m_values.Add(itemValue.Clone());
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
            AnnotationValueCollection annotationValueCollection = (AnnotationValueCollection)base.Clone();
            annotationValueCollection.m_values = new ArrayList(m_values.Count);
            foreach (object obj in m_values)
            {
                AnnotationValue annotationValue = (AnnotationValue)obj;
                annotationValueCollection.m_values.Add(annotationValue.Clone());
            }

            return annotationValueCollection;
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

        public void CopyTo(AnnotationValue[] array, int index)
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
                if (!typeof(AnnotationValue).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add AnnotationValue objects into the collection.");
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
            if (!typeof(AnnotationValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AnnotationValue objects into the collection.");
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
            if (!typeof(AnnotationValue).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add AnnotationValue objects into the collection.");
            }

            return m_values.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, AnnotationValue value)
        {
            Insert(index, value);
        }

        public void Remove(AnnotationValue value)
        {
            Remove(value);
        }

        public bool Contains(AnnotationValue value)
        {
            return Contains(value);
        }

        public int IndexOf(AnnotationValue value)
        {
            return IndexOf(value);
        }

        public int Add(AnnotationValue value)
        {
            return Add(value);
        }

        private ArrayList m_values = new ArrayList();

        private DateTime m_startTime = DateTime.MinValue;

        private DateTime m_endTime = DateTime.MinValue;

        private ResultID m_resultID = ResultID.S_OK;

        private string m_diagnosticInfo;
    }
}