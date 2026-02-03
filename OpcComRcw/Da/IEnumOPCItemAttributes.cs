using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [Guid("39c13a55-011e-11d0-9675-0020afd8adb3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IEnumOPCItemAttributes
    {
        void Next([MarshalAs(UnmanagedType.I4)] int celt, out IntPtr ppItemArray, [MarshalAs(UnmanagedType.I4)] out int pceltFetched);

        void Skip([MarshalAs(UnmanagedType.I4)] int celt);

        void Reset();

        void Clone(out IEnumOPCItemAttributes ppEnumItemAttributes);
    }
}