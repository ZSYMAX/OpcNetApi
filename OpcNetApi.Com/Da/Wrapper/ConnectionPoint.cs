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
            m_interface = iid;
            m_container = container;
        }

        public object Callback => m_callback;

        public bool IsConnected => m_callback != null;

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
                    if (m_callback != null)
                    {
                        throw new ExternalException("CONNECT_E_ADVISELIMIT", -2147220991);
                    }

                    m_callback = pUnkSink;
                    pdwCookie = ++m_cookie;
                    m_container.OnAdvise(m_interface);
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
                    if (m_cookie != dwCookie || m_callback == null)
                    {
                        throw new ExternalException("CONNECT_E_NOCONNECTION", -2147220992);
                    }

                    m_callback = null;
                    m_container.OnUnadvise(m_interface);
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
                    pIID = m_interface;
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
                    ppCPC = m_container;
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