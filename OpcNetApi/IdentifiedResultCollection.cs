using System;
using System.Collections;

namespace Opc
{
    [Serializable]
    public class IdentifiedResultCollection : ICloneable, ICollection, IEnumerable
    {
        public IdentifiedResult this[int index]
        {
            get { return this.m_results[index]; }
            set { this.m_results[index] = value; }
        }

        public IdentifiedResultCollection()
        {
        }

        public IdentifiedResultCollection(ICollection collection)
        {
            this.Init(collection);
        }

        public void Init(ICollection collection)
        {
            this.Clear();
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

                this.m_results = (IdentifiedResult[])arrayList.ToArray(typeof(IdentifiedResult));
            }
        }

        public void Clear()
        {
            this.m_results = new IdentifiedResult[0];
        }

        public virtual object Clone()
        {
            return new IdentifiedResultCollection(this);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_results == null)
                {
                    return 0;
                }

                return this.m_results.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_results != null)
            {
                this.m_results.CopyTo(array, index);
            }
        }

        public void CopyTo(IdentifiedResult[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_results.GetEnumerator();
        }

        private IdentifiedResult[] m_results = new IdentifiedResult[0];
    }
}