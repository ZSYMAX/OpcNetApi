using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom.Da.Wrapper
{
    [CLSCompliant(false)]
    public class EnumConnectionPoints : IEnumConnectionPoints
    {
        internal EnumConnectionPoints(ICollection connectionPoints)
        {
            if (connectionPoints != null)
            {
                foreach (object obj in connectionPoints)
                {
                    IConnectionPoint value = (IConnectionPoint)obj;
                    this.m_connectionPoints.Add(value);
                }
            }
        }

        public void Skip(int cConnections)
        {
            lock (this)
            {
                try
                {
                    this.m_index += cConnections;
                    if (this.m_index > this.m_connectionPoints.Count)
                    {
                        this.m_index = this.m_connectionPoints.Count;
                    }
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void Clone(out IEnumConnectionPoints ppenum)
        {
            lock (this)
            {
                try
                {
                    ppenum = new EnumConnectionPoints(this.m_connectionPoints);
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void Reset()
        {
            lock (this)
            {
                try
                {
                    this.m_index = 0;
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void RemoteNext(int cConnections, IntPtr ppCP, out int pcFetched)
        {
            lock (this)
            {
                try
                {
                    if (ppCP == IntPtr.Zero)
                    {
                        throw new ExternalException("E_INVALIDARG", -2147024809);
                    }

                    IntPtr[] array = new IntPtr[cConnections];
                    pcFetched = 0;
                    if (this.m_index < this.m_connectionPoints.Count)
                    {
                        int num = 0;
                        while (num < this.m_connectionPoints.Count - this.m_index && num < cConnections)
                        {
                            IConnectionPoint o = (IConnectionPoint)this.m_connectionPoints[this.m_index + num];
                            array[num] = Marshal.GetComInterfaceForObject(o, typeof(IConnectionPoint));
                            pcFetched++;
                            num++;
                        }

                        this.m_index += pcFetched;
                        Marshal.Copy(array, 0, ppCP, pcFetched);
                    }
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        private ArrayList m_connectionPoints = new ArrayList();

        private int m_index;
    }
}