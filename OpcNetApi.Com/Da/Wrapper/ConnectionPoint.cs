using System;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom.Da.Wrapper
{
    [CLSCompliant(false)]
    public class ConnectionPoint : IConnectionPoint
    {
        public ConnectionPoint(Guid iid, ConnectionPointContainer container)
        {
            this.m_interface = iid;
            this.m_container = container;
        }

        public object Callback
        {
            get { return this.m_callback; }
        }

        public bool IsConnected
        {
            get { return this.m_callback != null; }
        }

        public void Advise(object pUnkSink, out int pdwCookie)
        {
            lock (this)
            {
                try
                {
                    if (pUnkSink == null)
                    {
                        throw new ExternalException("E_POINTER", -2147467261);
                    }

                    pdwCookie = 0;
                    if (this.m_callback != null)
                    {
                        throw new ExternalException("CONNECT_E_ADVISELIMIT", -2147220991);
                    }

                    this.m_callback = pUnkSink;
                    pdwCookie = ++this.m_cookie;
                    this.m_container.OnAdvise(this.m_interface);
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void Unadvise(int dwCookie)
        {
            lock (this)
            {
                try
                {
                    if (this.m_cookie != dwCookie || this.m_callback == null)
                    {
                        throw new ExternalException("CONNECT_E_NOCONNECTION", -2147220992);
                    }

                    this.m_callback = null;
                    this.m_container.OnUnadvise(this.m_interface);
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void GetConnectionInterface(out Guid pIID)
        {
            lock (this)
            {
                try
                {
                    pIID = this.m_interface;
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void EnumConnections(out IEnumConnections ppenum)
        {
            throw new ExternalException("E_NOTIMPL", -2147467263);
        }

        public void GetConnectionPointContainer(out IConnectionPointContainer ppCPC)
        {
            lock (this)
            {
                try
                {
                    ppCPC = this.m_container;
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        private Guid m_interface = Guid.Empty;

        private ConnectionPointContainer m_container;

        private object m_callback;

        private int m_cookie;
    }
}