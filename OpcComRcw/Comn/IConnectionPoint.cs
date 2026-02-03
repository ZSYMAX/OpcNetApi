using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
    [ComImport]
    public interface IConnectionPoint
    {
        void GetConnectionInterface(out Guid pIID);

        void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

        void Advise([MarshalAs(UnmanagedType.IUnknown)] object pUnkSink, [MarshalAs(UnmanagedType.I4)] out int pdwCookie);

        void Unadvise([MarshalAs(UnmanagedType.I4)] int dwCookie);

        void EnumConnections(out IEnumConnections ppEnum);
    }
}