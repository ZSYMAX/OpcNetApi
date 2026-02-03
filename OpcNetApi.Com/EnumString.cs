using System;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom
{
    public class EnumString : IDisposable
    {
        public EnumString(object enumerator)
        {
            m_enumerator = (IEnumString)enumerator;
        }

        public void Dispose()
        {
            Interop.ReleaseServer(m_enumerator);
            m_enumerator = null;
        }

        public string[] Next(int count)
        {
            string[] result;
            try
            {
                IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(IntPtr)) * count);
                try
                {
                    m_enumerator.RemoteNext(count, intPtr, out var num);
                    if (num == 0)
                    {
                        result = new string[0];
                    }
                    else
                    {
                        result = Interop.GetUnicodeStrings(ref intPtr, num, true);
                    }
                }
                finally
                {
                    Marshal.FreeCoTaskMem(intPtr);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public void Skip(int count)
        {
            m_enumerator.Skip(count);
        }

        public void Reset()
        {
            m_enumerator.Reset();
        }

        public EnumString Clone()
        {
            m_enumerator.Clone(out var enumerator);
            return new EnumString(enumerator);
        }

        private IEnumString m_enumerator;
    }
}