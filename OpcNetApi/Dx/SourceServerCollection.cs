using System;
using System.Collections;

namespace Opc.Dx
{
    public class SourceServerCollection : ICollection, IEnumerable, ICloneable
    {
        public SourceServer this[int index]
        {
            get { return (SourceServer)this.m_servers[index]; }
        }

        public SourceServer this[string name]
        {
            get
            {
                foreach (object obj in this.m_servers)
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
            this.m_servers.Clear();
            if (sourceServers != null)
            {
                foreach (object obj in sourceServers)
                {
                    SourceServer value = (SourceServer)obj;
                    this.m_servers.Add(value);
                }
            }
        }

        public virtual object Clone()
        {
            SourceServerCollection sourceServerCollection = (SourceServerCollection)base.MemberwiseClone();
            sourceServerCollection.m_servers = new ArrayList();
            foreach (object obj in this.m_servers)
            {
                SourceServer sourceServer = (SourceServer)obj;
                sourceServerCollection.m_servers.Add(sourceServer.Clone());
            }

            return sourceServerCollection;
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public int Count
        {
            get
            {
                if (this.m_servers == null)
                {
                    return 0;
                }

                return this.m_servers.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (this.m_servers != null)
            {
                this.m_servers.CopyTo(array, index);
            }
        }

        public void CopyTo(SourceServer[] array, int index)
        {
            this.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_servers.GetEnumerator();
        }

        private ArrayList m_servers = new ArrayList();
    }
}