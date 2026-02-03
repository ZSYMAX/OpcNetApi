using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Comn
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct CONNECTDATA
    {
        [MarshalAs(UnmanagedType.IUnknown)]
        private object pUnk;

        [MarshalAs(UnmanagedType.I4)]
        private int dwCookie;
    }
}