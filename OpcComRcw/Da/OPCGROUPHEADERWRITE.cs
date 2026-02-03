using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCGROUPHEADERWRITE
    {
        [MarshalAs(UnmanagedType.I4)]
        public int dwItemCount;

        [MarshalAs(UnmanagedType.I4)]
        public int hClientGroup;

        [MarshalAs(UnmanagedType.I4)]
        public int dwTransactionID;

        [MarshalAs(UnmanagedType.I4)]
        public int hrStatus;
    }
}