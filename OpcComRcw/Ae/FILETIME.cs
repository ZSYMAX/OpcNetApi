using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Ae
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILETIME
    {
        public int dwLowDateTime;

        public int dwHighDateTime;
    }
}