using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [Guid("55C382C8-21C7-4e88-96C1-BECFB1E3F483")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCEnumGUID
    {
        void Next([MarshalAs(UnmanagedType.I4)] int celt, [Out] IntPtr rgelt, [MarshalAs(UnmanagedType.I4)] out int pceltFetched);

        void Skip([MarshalAs(UnmanagedType.I4)] int celt);

        void Reset();

        void Clone(out IOPCEnumGUID ppenum);
    }
}