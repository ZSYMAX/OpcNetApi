using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Da
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OPCITEMPROPERTY
    {
        [MarshalAs(UnmanagedType.I2)]
        public short vtDataType;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.I4)]
        public int dwPropertyID;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szItemID;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.Struct)]
        public object vValue;

        [MarshalAs(UnmanagedType.I4)]
        public int hrErrorID;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}