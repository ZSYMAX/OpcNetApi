using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdArgumentDefinition
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.I2)]
        public short vtValueType;

        [MarshalAs(UnmanagedType.I2)]
        public short wReserved;

        [MarshalAs(UnmanagedType.I4)]
        public int bOptional;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.Struct)]
        public object vDefaultValue;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szUnitType;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;

        [MarshalAs(UnmanagedType.Struct)]
        public object vLowLimit;

        [MarshalAs(UnmanagedType.Struct)]
        public object vHighLimit;
    }
}