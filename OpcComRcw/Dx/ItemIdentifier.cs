using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Dx
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ItemIdentifier
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szVersion;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}