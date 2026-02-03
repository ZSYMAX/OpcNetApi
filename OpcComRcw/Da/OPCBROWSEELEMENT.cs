using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCBROWSEELEMENT
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemID;

        [MarshalAs(UnmanagedType.I4)]
        public int dwFlagValue;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;

        public OPCITEMPROPERTIES ItemProperties;
    }
}