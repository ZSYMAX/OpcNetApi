using System;
using System.Collections;

namespace Opc.Dx
{
    public class DXConnectionQueryCollection : ICloneable, IList, ICollection, IEnumerable
    {
        public DXConnectionQuery this[int index]
        {
            get { return (DXConnectionQuery)this.m_queries[index]; }
        }

        public DXConnectionQuery this[string name]
        {
            get
            {
                foreach (object obj in this.m_queries)
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
            this.m_queries.Clear();
            if (queries != null)
            {
                foreach (object obj in queries)
                {
                    DXConnectionQuery value = (DXConnectionQuery)obj;
                    this.m_queries.Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            DXConnectionQueryCollection dxconnectionQueryCollection = (DXConnectionQueryCollection)base.MemberwiseClone();
            dxconnectionQueryCollection.m_queries = new ArrayList();
            foreach (object obj in this.m_queries)
            {
                DXConnectionQuery dxconnectionQuery = (DXConnectionQuery)obj;
                dxconnectionQueryCollection.m_queries.Add(dxconnectionQuery.Clone());
            }

            return dxconnectionQueryCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_queries == null)
                {
                    return 0;
                }

                return this.m_queries.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_queries != null)
            {
                this.m_queries.CopyTo(array, index);
            }
        }

        public void CopyTo(DXConnectionQuery[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_queries.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_queries[index]; }
            set { this.Insert(index, value); }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.m_queries.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            this.Remove(this.m_queries[index]);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(DXConnectionQuery).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DXConnectionQuery objects into the collection.");
            }

            this.m_queries.Insert(index, value);
        }

        public void Remove(object value)
        {
            if (!typeof(DXConnectionQuery).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only delete DXConnectionQuery obejcts from the collection.");
            }

            this.m_queries.Remove(value);
        }

        public bool Contains(object value)
        {
            foreach (object obj in this.m_queries)
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
            this.m_queries.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_queries.IndexOf(value);
        }

        public int Add(object value)
        {
            this.Insert(this.m_queries.Count, value);
            return this.m_queries.Count - 1;
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, DXConnectionQuery value)
        {
            this.Insert(index, value);
        }

        public void Remove(DXConnectionQuery value)
        {
            this.Remove(value);
        }

        public bool Contains(DXConnectionQuery value)
        {
            return this.Contains(value);
        }

        public int IndexOf(DXConnectionQuery value)
        {
            return this.IndexOf(value);
        }

        public int Add(DXConnectionQuery value)
        {
            return this.Add(value);
        }

        private ArrayList m_queries = new ArrayList();
    }
}