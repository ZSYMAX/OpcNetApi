using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [Guid("39c13a4e-011e-11d0-9675-0020afd8adb3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCServerPublicGroups
    {
        void GetPublicGroupByName([MarshalAs(UnmanagedType.LPWStr)] string szName, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown, IidParameterIndex = 1)] out object ppUnk);

        void RemovePublicGroup([MarshalAs(UnmanagedType.I4)] int hServerGroup, [MarshalAs(UnmanagedType.I4)] int bForce);
    }
}