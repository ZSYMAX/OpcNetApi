using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Security
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("7AA83A01-6C77-11d3-84F9-00008630A38B")]
    [ComImport]
    public interface IOPCSecurityNT
    {
        void IsAvailableNT([MarshalAs(UnmanagedType.I4)] out int pbAvailable);

        void QueryMinImpersonationLevel([MarshalAs(UnmanagedType.I4)] out int pdwMinImpLevel);

        void ChangeUser();
    }
}