using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCHDA_TIME
    {
        [MarshalAs(UnmanagedType.I4)]
        public int bString;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTime;

        public OPCHDA_FILETIME ftTime;
    }
}