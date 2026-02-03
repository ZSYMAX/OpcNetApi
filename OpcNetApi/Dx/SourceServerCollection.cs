using System;
using System.Collections;

namespace Opc.Dx
{
    public class SourceServerCollection : ICollection, IEnumerable, ICloneable
    {
        public SourceServer this[int index] => (SourceServer)m_servers[index];

        public SourceServer this[string name]
        {
            get
            {
                foreach (object obj in m_servers)
                {
                    SourceServer sourceServer = (SourceServer)obj;
                    if (sourceServer.Name == name)
                    {
                        return sourceServer;
                    }
                }

                return null;
            }
        }

        internal SourceServerCollection()
        {
        }

        internal void Initialize(ICollection sourceServers)
        {
            m_servers.Clear();
            if (sourceServers != null)
            {
                foreach (object obj in sourceServers)
                {
                    SourceServer value = (SourceServer)obj;
                    m_servers.Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            SourceServerCollection sourceServerCollection = (SourceServerCollection)MemberwiseClone();
            sourceServerCollection.m_servers = new ArrayList();
            foreach (object obj in m_servers)
            {
                SourceServer sourceServer = (SourceServer)obj;
                sourceServerCollection.m_servers.Add(sourceServer.Clone());
            }

            return sourceServerCollection;
        }

        public bool IsSynchronized => false;

        public int Count
        {
            get
            {
                if (m_servers == null)
                {
                    return 0;
                }

                return m_servers.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (m_servers != null)
            {
                m_servers.CopyTo(array, index);
            }
        }

        public void CopyTo(SourceServer[] array, int index)
        {
            CopyTo(array, index);
        }

        public object SyncRoot => this;

        public IEnumerator GetEnumerator()
        {
            return m_servers.GetEnumerator();
        }

        private ArrayList m_servers = new ArrayList();
    }
}