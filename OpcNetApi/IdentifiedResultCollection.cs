using System;
using System.Collections;

namespace Opc
{
    [Serializable]
    public class IdentifiedResultCollection : ICloneable, ICollection, IEnumerable
    {
        public IdentifiedResult this[int index]
        {
            get => m_results[index];
            set => m_results[index] = value;
        }

        public IdentifiedResultCollection()
        {
        }

        public IdentifiedResultCollection(ICollection collection)
        {
            Init(collection);
        }

        public void Init(ICollection collection)
        {
            Clear();
            if (collection != null)
            {
                ArrayList arrayList = new ArrayList(collection.Count);
                foreach (object obj in collection)
                {
                    if (typeof(IdentifiedResult).IsInstanceOfType(obj))
                    {
                        arrayList.Add(((IdentifiedResult)obj).Clone());
                    }
                }

                m_results = (IdentifiedResult[])arrayList.ToArray(typeof(IdentifiedResult));
            }
        }

        public void Clear()
        {
            m_results = new IdentifiedResult[0];
        }

        public virtual object Clone()
        {
            return new IdentifiedResultCollection(this);
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

                return m_results.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_results != null)
            {
                m_results.CopyTo(array, index);
            }
        }

        public void CopyTo(IdentifiedResult[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_results.GetEnumerator();
        }

        private IdentifiedResult[] m_results = new IdentifiedResult[0];
    }
}