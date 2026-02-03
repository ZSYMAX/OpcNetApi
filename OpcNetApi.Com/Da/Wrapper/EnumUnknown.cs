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
                    this.m_unknowns.Add(value);
                }
            }
        }

        public void Skip(int celt)
        {
            lock (this)
            {
                try
                {
                    this.m_index += celt;
                    if (this.m_index > this.m_unknowns.Count)
                    {
                        this.m_index = this.m_unknowns.Count;
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
                    ppenum = new EnumUnknown(this.m_unknowns);
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
                    if (this.m_index < this.m_unknowns.Count)
                    {
                        int num = 0;
                        while (num < this.m_unknowns.Count - this.m_index && num < array.Length)
                        {
                            array[num] = Marshal.GetIUnknownForObject(this.m_unknowns[this.m_index + num]);
                            pceltFetched++;
                            num++;
                        }

                        this.m_index += pceltFetched;
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