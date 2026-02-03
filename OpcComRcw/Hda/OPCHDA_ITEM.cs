using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCHDA_ITEM
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        [MarshalAs(UnmanagedType.I4)]
        public int haAggregate;

        [MarshalAs(UnmanagedType.I4)]
        public int dwCount;

        public IntPtr pftTimeStamps;

        public IntPtr pdwQualities;

        public IntPtr pvDataValues;
    }
}