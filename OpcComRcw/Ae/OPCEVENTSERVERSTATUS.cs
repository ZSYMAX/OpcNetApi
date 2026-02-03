using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Ae
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCEVENTSERVERSTATUS
    {
        public FILETIME ftStartTime;

        public FILETIME ftCurrentTime;

        public FILETIME ftLastUpdateTime;

        public OPCEVENTSERVERSTATE dwServerState;

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