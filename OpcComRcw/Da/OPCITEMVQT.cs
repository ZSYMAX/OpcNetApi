using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMVQT
    {
        [MarshalAs(UnmanagedType.Struct)]
        public object vDataValue;

        [MarshalAs(UnmanagedType.I4)]
        public int bQualitySpecified;

        [MarshalAs(UnmanagedType.I2)]
        public short wQuality;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.I4)]
        public int bTimeStampSpecified;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;

        public FILETIME ftTimeStamp;
    }
}