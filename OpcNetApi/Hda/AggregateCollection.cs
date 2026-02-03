using System;
using System.Collections;

namespace Opc.Hda
{
    [Serializable]
    public class AggregateCollection : ICloneable, ICollection, IEnumerable
    {
        public AggregateCollection()
        {
        }

        public AggregateCollection(ICollection collection)
        {
            this.Init(collection);
        }

        public Aggregate this[int index]
        {
            get { return this.m_aggregates[index]; }
            set { this.m_aggregates[index] = value; }
        }

        public Aggregate Find(int id)
        {
            foreach (Aggregate aggregate in this.m_aggregates)
            {
                if (aggregate.ID == id)
                {
                    return aggregate;
                }
            }

            return null;
        }

        public void Init(ICollection collection)
        {
            this.Clear();
            if (collection != null)
            {
                ArrayList arrayList = new ArrayList(collection.Count);
                foreach (object obj in collection)
                {
                    if (obj.GetType() == typeof(Aggregate))
                    {
                        arrayList.Add(Convert.Clone(obj));
                    }
                }

                this.m_aggregates = (Aggregate[])arrayList.ToArray(typeof(Aggregate));
            }
        }

        public void Clear()
        {
            this.m_aggregates = new Aggregate[0];
        }

        public virtual object Clone()
        {
            return new AggregateCollection(this);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_aggregates == null)
                {
                    return 0;
                }

                return this.m_aggregates.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_aggregates != null)
            {
                this.m_aggregates.CopyTo(array, index);
            }
        }

        public void CopyTo(Aggregate[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_aggregates.GetEnumerator();
        }

        private Aggregate[] m_aggregates = new Aggregate[0];
    }
}