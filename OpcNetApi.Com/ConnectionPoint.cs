using System;
using OpcRcw.Comn;

namespace OpcCom
{
    public class ConnectionPoint : IDisposable
    {
        public ConnectionPoint(object server, Guid iid)
        {
            ((IConnectionPointContainer)server).FindConnectionPoint(ref iid, out m_server);
        }

        public void Dispose()
        {
            if (m_server != null)
            {
                while (Unadvise() > 0)
                {
                }

                Interop.ReleaseServer(m_server);
                m_server = null;
            }
        }

        public int Cookie => m_cookie;

        public int Advise(object callback)
        {
            if (m_refs++ == 0)
            {
                m_server.Advise(callback, out m_cookie);
            }

            return m_refs;
        }

        public int Unadvise()
        {
            if (--m_refs == 0)
            {
                m_server.Unadvise(m_cookie);
            }

            return m_refs;
        }

        private IConnectionPoint m_server;

        private int m_cookie;

        private int m_refs;
    }
}