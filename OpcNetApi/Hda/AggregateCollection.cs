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
            Init(collection);
        }

        public Aggregate this[int index]
        {
            get => m_aggregates[index];
            set => m_aggregates[index] = value;
        }

        public Aggregate Find(int id)
        {
            foreach (Aggregate aggregate in m_aggregates)
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
            Clear();
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

                m_aggregates = (Aggregate[])arrayList.ToArray(typeof(Aggregate));
            }
        }

        public void Clear()
        {
            m_aggregates = new Aggregate[0];
        }

        public virtual object Clone()
        {
            return new AggregateCollection(this);
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_aggregates == null)
                {
                    return 0;
                }

                return m_aggregates.Length;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_aggregates != null)
            {
                m_aggregates.CopyTo(array, index);
            }
        }

        public void CopyTo(Aggregate[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_aggregates.GetEnumerator();
        }

        private Aggregate[] m_aggregates = new Aggregate[0];
    }
}