using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [Guid("F31DFDE1-07B6-11d2-B2D8-0060083BA1FB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCShutdown
    {
        void ShutdownRequest([MarshalAs(UnmanagedType.LPWStr)] string szReason);
    }
}