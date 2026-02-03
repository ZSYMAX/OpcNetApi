using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMDEF
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szAccessPath;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemID;

        [MarshalAs(UnmanagedType.I4)]
        public int bActive;

        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        [MarshalAs(UnmanagedType.I4)]
        public int dwBlobSize;

        public IntPtr pBlob;

        [MarshalAs(UnmanagedType.I2)]
        public short vtRequestedDataType;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;
    }
}