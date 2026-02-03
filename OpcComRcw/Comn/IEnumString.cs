using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000101-0000-0000-C000-000000000046")]
    [ComImport]
    public interface IEnumString
    {
        void RemoteNext([MarshalAs(UnmanagedType.I4)] int celt, IntPtr rgelt, [MarshalAs(UnmanagedType.I4)] out int pceltFetched);

        void Skip([MarshalAs(UnmanagedType.I4)] int celt);

        void Reset();

        void Clone(out IEnumString ppenum);
    }
}