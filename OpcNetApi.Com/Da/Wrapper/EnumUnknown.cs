using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom.Da.Wrapper
{
    [CLSCompliant(false)]
    public class EnumUnknown : IEnumUnknown
    {
        internal EnumUnknown(ICollection unknowns)
        {
            if (unknowns != null)
            {
                foreach (object value in unknowns)
                {
                    m_unknowns.Add(value);
                }
            }
        }

        public void Skip(int celt)
        {
            lock (this)
            {
                try
                {
                    m_index += celt;
                    if (m_index > m_unknowns.Count)
                    {
                        m_index = m_unknowns.Count;
                    }
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void Clone(out IEnumUnknown ppenum)
        {
            lock (this)
            {
                try
                {
                    ppenum = new EnumUnknown(m_unknowns);
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
                    m_index = 0;
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void RemoteNext(int celt, IntPtr rgelt, out int pceltFetched)
        {
            lock (this)
            {
                try
                {
                    if (rgelt == IntPtr.Zero)
                    {
                        throw new ExternalException("E_INVALIDARG", -2147024809);
                    }

                    IntPtr[] array = new IntPtr[celt];
                    pceltFetched = 0;
                    if (m_index < m_unknowns.Count)
                    {
                        int num = 0;
                        while (num < m_unknowns.Count - m_index && num < array.Length)
                        {
                            array[num] = Marshal.GetIUnknownForObject(m_unknowns[m_index + num]);
                            pceltFetched++;
                            num++;
                        }

                        m_index += pceltFetched;
                        Marshal.Copy(array, 0, rgelt, pceltFetched);
                    }
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        private ArrayList m_unknowns = new ArrayList();

        private int m_index;
    }
}