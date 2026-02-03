using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IEnumConnections
    {
        void RemoteNext([MarshalAs(UnmanagedType.I4)] int cConnections, [Out] IntPtr rgcd, [MarshalAs(UnmanagedType.I4)] out int pcFetched);

        void Skip([MarshalAs(UnmanagedType.I4)] int cConnections);

        void Reset();

        void Clone(out IEnumConnections ppEnum);
    }
}