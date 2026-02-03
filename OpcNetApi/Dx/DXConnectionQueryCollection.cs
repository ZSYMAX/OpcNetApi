using System;
using System.Collections;

namespace Opc.Dx
{
    public class DXConnectionQueryCollection : ICloneable, IList, ICollection, IEnumerable
    {
        public DXConnectionQuery this[int index] => (DXConnectionQuery)m_queries[index];

        public DXConnectionQuery this[string name]
        {
            get
            {
                foreach (object obj in m_queries)
                {
                    DXConnectionQuery dxconnectionQuery = (DXConnectionQuery)obj;
                    if (dxconnectionQuery.Name == name)
                    {
                        return dxconnectionQuery;
                    }
                }

                return null;
            }
        }

        internal DXConnectionQueryCollection()
        {
        }

        internal void Initialize(ICollection queries)
        {
            m_queries.Clear();
            if (queries != null)
            {
                foreach (object obj in queries)
                {
                    DXConnectionQuery value = (DXConnectionQuery)obj;
                    m_queries.Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            DXConnectionQueryCollection dxconnectionQueryCollection = (DXConnectionQueryCollection)MemberwiseClone();
            dxconnectionQueryCollection.m_queries = new ArrayList();
            foreach (object obj in m_queries)
            {
                DXConnectionQuery dxconnectionQuery = (DXConnectionQuery)obj;
                dxconnectionQueryCollection.m_queries.Add(dxconnectionQuery.Clone());
            }

            return dxconnectionQueryCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_queries == null)
                {
                    return 0;
                }

                return m_queries.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_queries != null)
            {
                m_queries.CopyTo(array, index);
            }
        }

        public void CopyTo(DXConnectionQuery[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_queries.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_queries[index];
            set => Insert(index, value);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= m_queries.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            Remove(m_queries[index]);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(DXConnectionQuery).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DXConnectionQuery objects into the collection.");
            }

            m_queries.Insert(index, value);
        }

        public void Remove(object value)
        {
            if (!typeof(DXConnectionQuery).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only delete DXConnectionQuery obejcts from the collection.");
            }

            m_queries.Remove(value);
        }

        public bool Contains(object value)
        {
            foreach (object obj in m_queries)
            {
                ItemIdentifier itemIdentifier = (ItemIdentifier)obj;
                if (itemIdentifier.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            m_queries.Clear();
        }

        public int IndexOf(object value)
        {
            return m_queries.IndexOf(value);
        }

        public int Add(object value)
        {
            Insert(m_queries.Count, value);
            return m_queries.Count - 1;
        }

        public bool IsFixedSize => false;

        public void Insert(int index, DXConnectionQuery value)
        {
            Insert(index, value);
        }

        public void Remove(DXConnectionQuery value)
        {
            Remove(value);
        }

        public bool Contains(DXConnectionQuery value)
        {
            return Contains(value);
        }

        public int IndexOf(DXConnectionQuery value)
        {
            return IndexOf(value);
        }

        public int Add(DXConnectionQuery value)
        {
            return Add(value);
        }

        private ArrayList m_queries = new ArrayList();
    }
}