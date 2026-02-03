using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class ResultCollection : ItemIdentifier, ICloneable, IList, ICollection, IEnumerable
    {
        public Result this[int index]
        {
            get => (Result)m_results[index];
            set => m_results[index] = value;
        }

        public ResultCollection()
        {
        }

        public ResultCollection(ItemIdentifier item) : base(item)
        {
        }

        public ResultCollection(ResultCollection item) : base(item)
        {
            m_results = new ArrayList(item.m_results.Count);
            foreach (object obj in item.m_results)
            {
                Result result = (Result)obj;
                m_results.Add(result.Clone());
            }
        }

        public override object Clone()
        {
            ResultCollection resultCollection = (ResultCollection)base.Clone();
            resultCollection.m_results = new ArrayList(m_results.Count);
            foreach (object obj in m_results)
            {
                ResultCollection resultCollection2 = (ResultCollection)obj;
                resultCollection.m_results.Add(resultCollection2.Clone());
            }

            return resultCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_results == null)
                {
                    return 0;
                }

                return m_results.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_results != null)
            {
                m_results.CopyTo(array, index);
            }
        }

        public void CopyTo(Result[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_results.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_results[index];
            set
            {
                if (!typeof(Result).IsInstanceOfType(value))
                {
                    throw new ArgumentException("May only add Result objects into the collection.");
                }

                m_results[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            m_results.RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(Result).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Result objects into the collection.");
            }

            m_results.Insert(index, value);
        }

        public void Remove(object value)
        {
            m_results.Remove(value);
        }

        public bool Contains(object value)
        {
            return m_results.Contains(value);
        }

        public void Clear()
        {
            m_results.Clear();
        }

        public int IndexOf(object value)
        {
            return m_results.IndexOf(value);
        }

        public int Add(object value)
        {
            if (!typeof(Result).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add Result objects into the collection.");
            }

            return m_results.Add(value);
        }

        public bool IsFixedSize => false;

        public void Insert(int index, Result value)
        {
            Insert(index, value);
        }

        public void Remove(Result value)
        {
            Remove(value);
        }

        public bool Contains(Result value)
        {
            return Contains(value);
        }

        public int IndexOf(Result value)
        {
            return IndexOf(value);
        }

        public int Add(Result value)
        {
            return Add(value);
        }

        private ArrayList m_results = new ArrayList();
    }
}