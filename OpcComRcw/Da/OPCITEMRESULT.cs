using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMRESULT
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hServer;

        [MarshalAs(UnmanagedType.I2)]
        public short vtCanonicalDataType;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.I4)]
        public int dwAccessRights;

        [MarshalAs(UnmanagedType.I4)]
        public int dwBlobSize;

        public IntPtr pBlob;
    }
}