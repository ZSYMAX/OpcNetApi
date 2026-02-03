using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Dx
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DXGeneralResponse
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szConfigurationVersion;

        [MarshalAs(UnmanagedType.I4)]
        public int dwCount;

        public IntPtr pIdentifiedResults;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}