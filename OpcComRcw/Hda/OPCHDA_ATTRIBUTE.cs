using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCHDA_ATTRIBUTE
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNumValues;

        [MarshalAs(UnmanagedType.I4)]
        public int dwAttributeID;

        public IntPtr ftTimeStamps;

        public IntPtr vAttributeValues;
    }
}