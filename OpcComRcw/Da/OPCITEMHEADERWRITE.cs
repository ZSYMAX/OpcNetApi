using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMHEADERWRITE
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        [MarshalAs(UnmanagedType.I4)]
        public int dwError;
    }
}