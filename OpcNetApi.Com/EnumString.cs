using System;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom
{
    public class EnumString : IDisposable
    {
        public EnumString(object enumerator)
        {
            this.m_enumerator = (IEnumString)enumerator;
        }

        public void Dispose()
        {
            Interop.ReleaseServer(this.m_enumerator);
            this.m_enumerator = null;
        }

        public string[] Next(int count)
        {
            string[] result;
            try
            {
                IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(IntPtr)) * count);
                try
                {
                    int num = 0;
                    this.m_enumerator.RemoteNext(count, intPtr, out num);
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
            this.m_enumerator.Skip(count);
        }

        public void Reset()
        {
            this.m_enumerator.Reset();
        }

        public EnumString Clone()
        {
            IEnumString enumerator = null;
            this.m_enumerator.Clone(out enumerator);
            return new EnumString(enumerator);
        }

        private IEnumString m_enumerator;
    }
}