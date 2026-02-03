using System;
using OpcRcw.Comn;

namespace OpcCom
{
    public class ConnectionPoint : IDisposable
    {
        public ConnectionPoint(object server, Guid iid)
        {
            ((IConnectionPointContainer)server).FindConnectionPoint(ref iid, out this.m_server);
        }

        public void Dispose()
        {
            if (this.m_server != null)
            {
                while (this.Unadvise() > 0)
                {
                }

                Interop.ReleaseServer(this.m_server);
                this.m_server = null;
            }
        }

        public int Cookie
        {
            get { return this.m_cookie; }
        }

        public int Advise(object callback)
        {
            if (this.m_refs++ == 0)
            {
                this.m_server.Advise(callback, out this.m_cookie);
            }

            return this.m_refs;
        }

        public int Unadvise()
        {
            if (--this.m_refs == 0)
            {
                this.m_server.Unadvise(this.m_cookie);
            }

            return this.m_refs;
        }

        private IConnectionPoint m_server;

        private int m_cookie;

        private int m_refs;
    }
}