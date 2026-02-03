using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc.Dx
{
    [Serializable]
    public class DXConnectionCollection : ICloneable, IList, ICollection, IEnumerable, ISerializable
    {
        public DXConnection this[int index] => (DXConnection)m_connections[index];

        public DXConnection[] ToArray()
        {
            return (DXConnection[])m_connections.ToArray(typeof(DXConnection));
        }

        internal DXConnectionCollection()
        {
        }

        internal DXConnectionCollection(ICollection connections)
        {
            if (connections != null)
            {
                foreach (object obj in connections)
                {
                    DXConnection value = (DXConnection)obj;
                    m_connections.Add(value);
                }
            }
        }

        protected DXConnectionCollection(SerializationInfo info, StreamingContext context)
        {
            DXConnection[] array = (DXConnection[])info.GetValue("Connections", typeof(DXConnection[]));
            if (array != null)
            {
                foreach (DXConnection value in array)
                {
                    m_connections.Add(value);
                }
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            DXConnection[] array = null;
            if (m_connections.Count > 0)
            {
                array = new DXConnection[m_connections.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (DXConnection)m_connections[i];
                }
            }

            info.AddValue("Connections", array);
        }

        public virtual object Clone()
        {
            DXConnectionCollection dxconnectionCollection = (DXConnectionCollection)MemberwiseClone();
            dxconnectionCollection.m_connections = new ArrayList();
            foreach (object obj in m_connections)
            {
                DXConnection dxconnection = (DXConnection)obj;
                dxconnectionCollection.m_connections.Add(dxconnection.Clone());
            }

            return dxconnectionCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_connections == null)
                {
                    return 0;
                }

                return m_connections.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_connections != null)
            {
                m_connections.CopyTo(array, index);
            }
        }

        public void CopyTo(DXConnection[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_connections.GetEnumerator();
        }

        public bool IsReadOnly => false;

        object IList.this[int index]
        {
            get => m_connections[index];
            set => Insert(index, value);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= m_connections.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            Remove(m_connections[index]);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(DXConnection).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DXConnection objects into the collection.");
            }

            m_connections.Insert(index, (DXConnection)value);
        }

        public void Remove(object value)
        {
            if (!typeof(ItemIdentifier).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only delete Opc.Dx.ItemIdentifier obejcts from the collection.");
            }

            foreach (object obj in m_connections)
            {
                ItemIdentifier itemIdentifier = (ItemIdentifier)obj;
                if (itemIdentifier.Equals(value))
                {
                    m_connections.Remove(itemIdentifier);
                    break;
                }
            }
        }

        public bool Contains(object value)
        {
            foreach (object obj in m_connections)
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
            m_connections.Clear();
        }

        public int IndexOf(object value)
        {
            return m_connections.IndexOf(value);
        }

        public int Add(object value)
        {
            Insert(m_connections.Count, value);
            return m_connections.Count - 1;
        }

        public bool IsFixedSize => false;

        public void Insert(int index, DXConnection value)
        {
            Insert(index, value);
        }

        public void Remove(DXConnection value)
        {
            Remove(value);
        }

        public bool Contains(DXConnection value)
        {
            return Contains(value);
        }

        public int IndexOf(DXConnection value)
        {
            return IndexOf(value);
        }

        public int Add(DXConnection value)
        {
            return Add(value);
        }

        private ArrayList m_connections = new ArrayList();

        private class Names
        {
            internal const string CONNECTIONS = "Connections";
        }
    }
}