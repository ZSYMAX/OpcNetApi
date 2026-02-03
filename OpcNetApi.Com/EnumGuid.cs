using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpcRcw.Comn;

namespace OpcCom
{
    public class EnumGuid
    {
        public EnumGuid(object server)
        {
            this.m_enumerator = (IEnumGUID)server;
        }

        public void Release()
        {
            Interop.ReleaseServer(this.m_enumerator);
            this.m_enumerator = null;
        }

        public object GetEnumerator()
        {
            return this.m_enumerator;
        }

        public Guid[] GetAll()
        {
            this.Reset();
            ArrayList arrayList = new ArrayList();
            for (;;)
            {
                Guid[] array = this.Next(1);
                if (array == null)
                {
                    break;
                }

                arrayList.AddRange(array);
            }

            return (Guid[])arrayList.ToArray(typeof(Guid));
        }

        public Guid[] Next(int count)
        {
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Guid)) * count);
            Guid[] result;
            try
            {
                int num = 0;
                try
                {
                    this.m_enumerator.Next(count, intPtr, out num);
                }
                catch (Exception)
                {
                    return null;
                }

                if (num == 0)
                {
                    result = null;
                }
                else
                {
                    IntPtr ptr = intPtr;
                    Guid[] array = new Guid[num];
                    for (int i = 0; i < num; i++)
                    {
                        array[i] = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                        ptr = (IntPtr)(ptr.ToInt64() + (long)Marshal.SizeOf(typeof(Guid)));
                    }

                    result = array;
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(intPtr);
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

        public EnumGuid Clone()
        {
            IEnumGUID server = null;
            this.m_enumerator.Clone(out server);
            return new EnumGuid(server);
        }

        private IEnumGUID m_enumerator;
    }
}