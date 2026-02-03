using System;
using System.Runtime.InteropServices;

namespace OpcRcw.Cmd
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OpcCmdNamespaceDefinition
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szUri;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string szDescription;

        [MarshalAs(UnmanagedType.I4)]
        public int dwNoOfCommandNames;

        public IntPtr pszCommandNames;
    }
}