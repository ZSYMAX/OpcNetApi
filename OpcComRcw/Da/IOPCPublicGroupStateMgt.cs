using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [Guid("39c13a51-011e-11d0-9675-0020afd8adb3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCPublicGroupStateMgt
    {
        void GetState([MarshalAs(UnmanagedType.I4)] out int pPublic);

        void MoveToPublic();
    }
}