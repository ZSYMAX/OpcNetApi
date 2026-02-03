using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCSERVERSTATUS
    {
        public FILETIME ftStartTime;

        public FILETIME ftCurrentTime;

        public FILETIME ftLastUpdateTime;

        public OPCSERVERSTATE dwServerState;

        [MarshalAs(UnmanagedType.I4)]
        public int dwGroupCount;

        [MarshalAs(UnmanagedType.I4)]
        public int dwBandWidth;

        [MarshalAs(UnmanagedType.I2)]
        public short wMajorVersion;

        [MarshalAs(UnmanagedType.I2)]
        public short wMinorVersion;

        [MarshalAs(UnmanagedType.I2)]
        public short wBuildNumber;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szVendorInfo;
    }
}