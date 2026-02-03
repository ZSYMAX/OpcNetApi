using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom.Da.Wrapper
{
    [CLSCompliant(false)]
    public class EnumString : IEnumString
    {
        internal EnumString(ICollection strings)
        {
            if (strings != null)
            {
                foreach (object value in strings)
                {
                    this.m_strings.Add(value);
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
                    if (this.m_index > this.m_strings.Count)
                    {
                        this.m_index = this.m_strings.Count;
                    }
                }
                catch (Exception e)
                {
                    throw Server.CreateException(e);
                }
            }
        }

        public void Clone(out IEnumString ppenum)
        {
            lock (this)
            {
                try
                {
                    ppenum = new EnumString(this.m_strings);
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
                    if (this.m_index < this.m_strings.Count)
                    {
                        int num = 0;
                        while (num < this.m_strings.Count - this.m_index && num < array.Length)
                        {
                            array[num] = Marshal.StringToCoTaskMemUni((string)this.m_strings[this.m_index + num]);
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

        private ArrayList m_strings = new ArrayList();

        private int m_index;
    }
}