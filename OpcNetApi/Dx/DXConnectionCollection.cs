using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Opc.Dx
{
    [Serializable]
    public class DXConnectionCollection : ICloneable, IList, ICollection, IEnumerable, ISerializable
    {
        public DXConnection this[int index]
        {
            get { return (DXConnection)this.m_connections[index]; }
        }

        public DXConnection[] ToArray()
        {
            return (DXConnection[])this.m_connections.ToArray(typeof(DXConnection));
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
                    this.m_connections.Add(value);
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
                    this.m_connections.Add(value);
                }
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            DXConnection[] array = null;
            if (this.m_connections.Count > 0)
            {
                array = new DXConnection[this.m_connections.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (DXConnection)this.m_connections[i];
                }
            }

            info.AddValue("Connections", array);
        }

        public virtual object Clone()
        {
            DXConnectionCollection dxconnectionCollection = (DXConnectionCollection)base.MemberwiseClone();
            dxconnectionCollection.m_connections = new ArrayList();
            foreach (object obj in this.m_connections)
            {
                DXConnection dxconnection = (DXConnection)obj;
                dxconnectionCollection.m_connections.Add(dxconnection.Clone());
            }

            return dxconnectionCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_connections == null)
                {
                    return 0;
                }

                return this.m_connections.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_connections != null)
            {
                this.m_connections.CopyTo(array, index);
            }
        }

        public void CopyTo(DXConnection[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_connections.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this.m_connections[index]; }
            set { this.Insert(index, value); }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.m_connections.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            this.Remove(this.m_connections[index]);
        }

        public void Insert(int index, object value)
        {
            if (!typeof(DXConnection).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only add DXConnection objects into the collection.");
            }

            this.m_connections.Insert(index, (DXConnection)value);
        }

        public void Remove(object value)
        {
            if (!typeof(ItemIdentifier).IsInstanceOfType(value))
            {
                throw new ArgumentException("May only delete Opc.Dx.ItemIdentifier obejcts from the collection.");
            }

            foreach (object obj in this.m_connections)
            {
                ItemIdentifier itemIdentifier = (ItemIdentifier)obj;
                if (itemIdentifier.Equals(value))
                {
                    this.m_connections.Remove(itemIdentifier);
                    break;
                }
            }
        }

        public bool Contains(object value)
        {
            foreach (object obj in this.m_connections)
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
            this.m_connections.Clear();
        }

        public int IndexOf(object value)
        {
            return this.m_connections.IndexOf(value);
        }

        public int Add(object value)
        {
            this.Insert(this.m_connections.Count, value);
            return this.m_connections.Count - 1;
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Insert(int index, DXConnection value)
        {
            this.Insert(index, value);
        }

        public void Remove(DXConnection value)
        {
            this.Remove(value);
        }

        public bool Contains(DXConnection value)
        {
            return this.Contains(value);
        }

        public int IndexOf(DXConnection value)
        {
            return this.IndexOf(value);
        }

        public int Add(DXConnection value)
        {
            return this.Add(value);
        }

        private ArrayList m_connections = new ArrayList();

        private class Names
        {
            internal const string CONNECTIONS = "Connections";
        }
    }
}