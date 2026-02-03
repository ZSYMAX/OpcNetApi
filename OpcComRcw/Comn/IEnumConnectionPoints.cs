using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IEnumConnectionPoints
    {
        void RemoteNext([MarshalAs(UnmanagedType.I4)] int cConnections, [Out] IntPtr ppCP, [MarshalAs(UnmanagedType.I4)] out int pcFetched);

        void Skip([MarshalAs(UnmanagedType.I4)] int cConnections);

        void Reset();

        void Clone(out IEnumConnectionPoints ppEnum);
    }
}