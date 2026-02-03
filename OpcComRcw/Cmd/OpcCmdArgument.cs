using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdArgument
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.Struct)]
        public object vValue;
    }
}