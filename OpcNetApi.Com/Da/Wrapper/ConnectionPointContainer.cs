using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom.Da.Wrapper
{
    [CLSCompliant(false)]
    public class ConnectionPointContainer : IConnectionPointContainer
    {
        public virtual void OnAdvise(Guid riid)
        {
        }

        public virtual void OnUnadvise(Guid riid)
        {
        }

        protected ConnectionPointContainer()
        {
        }

        protected void RegisterInterface(Guid iid)
        {
            m_connectionPoints[iid] = new ConnectionPoint(iid, this);
        }

        protected void UnregisterInterface(Guid iid)
        {
            m_connectionPoints.Remove(iid);
        }

        protected object GetCallback(Guid iid)
        {
            ConnectionPoint connectionPoint = (ConnectionPoint)m_connectionPoints[iid];
            if (connectionPoint != null)
            {
                return connectionPoint.Callback;
            }

            return null;
        }

        protected bool IsConnected(Guid iid)
        {
            ConnectionPoint connectionPoint = (ConnectionPoint)m_connectionPoints[iid];
            return connectionPoint != null && connectionPoint.IsConnected;
        }

        public void EnumConnectionPoints(out IEnumConnectionPoints ppenum)
        {
            lock (this)
            {
                try
                {
                    ppenum = new EnumConnectionPoints(m_connectionPoints.Values);
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void FindConnectionPoint(ref Guid riid, out IConnectionPoint ppCP)
        {
            lock (this)
            {
                try
                {
                    ppCP = null;
                    ConnectionPoint connectionPoint = (ConnectionPoint)m_connectionPoints[riid];
                    if (connectionPoint == null)
                    {
                        throw new ExternalException("CONNECT_E_NOCONNECTION", -2147220992);
                    }

                    ppCP = connectionPoint;
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        private Hashtable m_connectionPoints = new Hashtable();
    }
}