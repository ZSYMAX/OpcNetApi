using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Hda
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCHDA_FILETIME
    {
        public int dwLowDateTime;

        public int dwHighDateTime;
    }
}