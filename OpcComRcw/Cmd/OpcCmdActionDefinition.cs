using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdActionDefinition
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szEventName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szInArguments;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szOutArguments;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}