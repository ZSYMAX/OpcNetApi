using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdStateDefinition
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDataTypeDefinition;

        [MarshalAs(UnmanagedType.I4)]
        public int dwReserved;
    }
}