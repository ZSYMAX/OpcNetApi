using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMSTATE
    {
        [MarshalAs(UnmanagedType.I4)]
        public int hClient;

        public FILETIME ftTimeStamp;

        [MarshalAs(UnmanagedType.I2)]
        public short wQuality;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.Struct)]
        public object vDataValue;
    }
}