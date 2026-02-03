using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMHEADER1
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        [MarshalAs(UnmanagedType.I4)]
        public int dwValueOffset;

        [MarshalAs(UnmanagedType.I2)]
        public short wQuality;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        public FILETIME ftTimeStampItem;
    }
}