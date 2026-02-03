using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [Guid("8E368666-D72E-4f78-87ED-647611C61C9F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IOPCGroupStateMgt2
    {
        void GetState([MarshalAs(UnmanagedType.I4)] out int pUpdateRate, [MarshalAs(UnmanagedType.I4)] out int pActive, [MarshalAs(UnmanagedType.LPWStr)] out string ppName, [MarshalAs(UnmanagedType.I4)] out int pTimeBias,
            [MarshalAs(UnmanagedType.R4)] out float pPercentDeadband, [MarshalAs(UnmanagedType.I4)] out int pLCID, [MarshalAs(UnmanagedType.I4)] out int phClientGroup, [MarshalAs(UnmanagedType.I4)] out int phServerGroup);

        void SetState(IntPtr pRequestedUpdateRate, [MarshalAs(UnmanagedType.I4)] out int pRevisedUpdateRate, IntPtr pActive, IntPtr pTimeBias, IntPtr pPercentDeadband, IntPtr pLCID, IntPtr phClientGroup);

        void SetName([MarshalAs(UnmanagedType.LPWStr)] string szName);

        void CloneGroup([MarshalAs(UnmanagedType.LPWStr)] string szName, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown, IidParameterIndex = 1)] out object ppUnk);

        void SetKeepAlive([MarshalAs(UnmanagedType.I4)] int dwKeepAliveTime, [MarshalAs(UnmanagedType.I4)] out int pdwRevisedKeepAliveTime);

        void GetKeepAlive([MarshalAs(UnmanagedType.I4)] out int pdwKeepAliveTime);
    }
}