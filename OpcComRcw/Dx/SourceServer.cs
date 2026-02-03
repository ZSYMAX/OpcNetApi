using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Dx
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SourceServer
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint dwMask;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szVersion;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szServerType;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szServerURL;

        [MarshalAs(UnmanagedType.I4)]
        public int bDefaultSourceServerConnected;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}