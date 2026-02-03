using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMPROPERTIES
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hrErrorID;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNumProperties;

        public IntPtr pItemProperties;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}