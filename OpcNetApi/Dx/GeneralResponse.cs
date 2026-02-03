using System;
using System.Collections;

namespace Opc.Dx
{
    [Serializable]
    public class GeneralResponse : ICloneable, ICollection, IEnumerable
    {
        public string Version
        {
            get { return this.m_version; }
            set { this.m_version = value; }
        }

        public GeneralResponse()
        {
        }

        public GeneralResponse(string version, ICollection results)
        {
            this.Version = version;
            this.Init(results);
        }

        public IdentifiedResult this[int index]
        {
            get { return this.m_results[index]; }
            set { this.m_results[index] = value; }
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

        private void Init(ICollection collection)
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

        private string m_version;

        private IdentifiedResult[] m_results = new IdentifiedResult[0];
    }
}